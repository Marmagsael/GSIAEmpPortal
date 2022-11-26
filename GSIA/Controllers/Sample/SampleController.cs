using LibraryMySql.DataAccess.Pis;
using LibraryMySql.Models.Pis;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace GSIA.Controllers.Sample; 

public class SampleController : Controller
{
    private readonly IWebHostEnvironment _env;
    private readonly Empmas _empmas; 

    public SampleController(IWebHostEnvironment env, Empmas empmas)
    {
        _env = env;
        _empmas = empmas;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ImageDragAndDrop()
    {
        return View();
    }

    public IActionResult DragDrop()
    {
        return View();
    }

    public JsonResult GetAllEmpBasicList()
    {
        Task<List<EmpmasBasicModel>> empmasBasicList = _empmas.Get();
        return Json(empmasBasicList); 
    }



    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public Task<string> UploadImages() {

        return Task.Run( () => {

            var dir = _env.WebRootPath + "\\uploads"; 

            try {
                if(!Directory.Exists(dir))
                Directory.CreateDirectory(dir); 
            }
            catch (Exception ex) {
                Response.StatusCode = 500; 
                return ex.Message.ToString(); 
            }

            var ret = string.Empty; //return value 

            for (int i = 0; i < Request.Form.Files.Count; i++ ) {
                if (Request.Form.Files[i].Length > 0 && 
                    Request.Form.Files[i].ContentType.ToLower().StartsWith("image/")) {

                    try {
                        var file = "\\" + System.Guid.NewGuid().ToString("N") + "-" + Request.Form.Files[i].FileName; 
                        using( FileStream fs = new FileStream(dir + file, FileMode.Create, FileAccess.Write)) {

                            const int bufsize = 2048000; 
                            byte[] buffer = new byte[bufsize]; 
                            using(Stream stream = Request.Form.Files[i].OpenReadStream()) {
                                int b = stream.Read(buffer,0,bufsize); 
                                int written = b; 

                                while (b > 0 ) {
                                    fs.Write(buffer,0,b); 
                                    b = stream.Read(buffer,0,bufsize); 
                                    written += b; 
                                }
                            }
                        }

                        ret += (i > 0 ? "|" : "") + "\\uploads" + file; // separate file with a | because it is less code than JSON  

                    }
                    catch(Exception ex) {
                        Response.StatusCode = 500; 
                        return ex.Message.ToString(); 
                    }
                }
            }

            return ret; 

        }); 
    }


    public IActionResult PartialView()
    {
        return View();
    }


}
