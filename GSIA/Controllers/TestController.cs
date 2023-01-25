using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using GSIA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Test1.Models;

namespace GSIA.Controllers;

public class TestController : Controller
{

    private const string PathToServiceAccountKeyFile = "client_id.json";
    private const string ParentId = "1wXOhLFaWgNSh0U4eW4W9FBqGbKcST4Mu";

    //public IActionResult Index()
    //{
    //    return View();
    ////}
    //[AllowAnonymous]
    //[HttpPost("upload")]
    //public async Task<IActionResult> UploadFiles(List<IFormFile> postedFiles)
    //{

    //    //Initialize Service Account
    //    var credential = GoogleCredential.FromFile(PathToServiceAccountKeyFile)
    //        .CreateScoped(DriveService.ScopeConstants.Drive);


    //    // Create Drive Service
    //    var service = new DriveService(new BaseClientService.Initializer()
    //    {
    //        HttpClientInitializer = credential
    //    });

    //    string folderName = "dith";

    //    //Check if File Already Exists
    //    var folderId = GetFolderList(folderName, service);

    //    var uploadedFileId = "";
    //    string encodedFileName = "";
    //    int fileSize = 0;

    //    FileInfo imageInfo = new("img.jpg");
    //    fileSize = ((int)imageInfo.Length);



    //    // Check file type and size ---
    //    bool isFileTypeValid = CheckFileTypeValidity("img.jpg");
    //    bool isFileSizeValid = CheckFileSizeValidity(fileSize);

    //    //Console.WriteLine(isFileTypeValid);



    //    if (isFileTypeValid && isFileSizeValid)
    //    {
    //        //Create File
    //        //Set Metadata
    //        var metadata = new Google.Apis.Drive.v3.Data.File()
    //        {
    //            Name = "img.jpg",
    //            MimeType = "image/jpg",
    //            Parents = new string[] { "1_3qa5RQXxA1sKqKeTbgfPhHRvuK5bIWD" }
    //        };

    //        // Load actual file into a file stream 
    //        await using (var media = new FileStream("img.jpg", FileMode.Open, FileAccess.Read))
    //        {
    //            //Create File on Google Drive with metada and filestream
    //            var request = service.Files.Create(metadata, media, "image/jpg");

    //            request.Fields = "*";
    //            // Upload file
    //            var result = await request.UploadAsync(CancellationToken.None);

    //            uploadedFileId = request.ResponseBody.Id;


    //            if (result.Status == Google.Apis.Upload.UploadStatus.Failed)
    //            {
    //                Console.WriteLine("Error");
    //            }
    //            else
    //            {
    //                //Find the file that was just uploaded
    //                var uploadedFile = await service.Files.Get(uploadedFileId).ExecuteAsync();

    //                //Convert Image to base 64
    //                byte[] imageArray = System.IO.File.ReadAllBytes(uploadedFile.Name);
    //                encodedFileName = Convert.ToBase64String(imageArray);
    //            }
    //        }
    //        ViewData["EncodedFileName"] = folderId;
    //        return View("~/Views/Home/Index.cshtml");
    //    }
    //    else
    //    {
    //        // Display Error message
    //        return View("~/Views/Home/Index.cshtml");
    //    }




    //}

    //public StorageModel GetFolderList(string folderName, DriveService service)
    //{

    //    string ParentId = "1wXOhLFaWgNSh0U4eW4W9FBqGbKcST4Mu";
    //    StorageModel item = new();
    //    FilesResource.ListRequest fileList = service.Files.List();

    //    fileList.Fields = "nextPageToken,files(id,name)";
    //    fileList.Q = $" mimeType = 'application/vnd.google-apps.folder' and name = '{folderName}' and trashed =  false and '{ParentId}' in parents ";
    //    IList<Google.Apis.Drive.v3.Data.File> files = fileList.Execute().Files;

    //    var item1 = files.FirstOrDefault(f => f.Name == folderName);
    //    item.Id = item1.Id;
    //    item.Name = item1.Name;
    //    return item;
    //}


    //public bool CheckFileTypeValidity(string file)
    //{

    //    string str = file;
    //    int index = str.LastIndexOf(".");
    //    string fileType = str.Substring(index);


    //    bool isValid = false;
    //    switch (fileType)
    //    {
    //        case ".jpg":
    //            isValid = true;
    //            break;
    //        case ".jpeg":
    //            isValid = true;
    //            break;

    //        case ".tiff":
    //            isValid = true;
    //            break;
    //        case ".png":
    //            isValid = true;
    //            break;
    //        case "pdf":
    //            isValid = true;
    //            break;
    //        default:
    //            isValid = false;
    //            break;
    //    }

    //    return isValid;


    //}

    //public bool CheckFileSizeValidity(int fileSize)
    //{

    //    int fileSizeToMb = fileSize / 1024 / 1024;

    //    int validSize = 8;

    //    if (fileSizeToMb > validSize)
    //    {
    //        return false;
    //    }
    //    return true;
    //}
    //public IActionResult Privacy()
    //{
    //    return View();
    //}

    //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    //public IActionResult Error()
    //{
    //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    //}
}
