using System.ComponentModel.DataAnnotations;

namespace GSIA.Models.Pis;

public class EmpmasUiModel
{
    [Required(ErrorMessage = "Enter a value")]
    [Display(Name = "Employee Number")]
    [StringLength(5, ErrorMessage = "This field must be 5 long.")]
    public string? Empnumber { get; set; }

    [Required(ErrorMessage = "Enter a value")]
    [Display(Name = "Last Name")]
    [StringLength(15, ErrorMessage = "This field must be 15 long.")]
    public string? Emplastnm { get; set; }

    [Required(ErrorMessage = "Enter a value")]
    [Display(Name = "First Name")]
    [StringLength(17, ErrorMessage = "This field must be 17 long.")]
    public string? Empfirstnm { get; set; }

    [Required(ErrorMessage = "Enter a value")]
    [Display(Name = "Middle Name")]
    [StringLength(15, ErrorMessage = "This field must be 15 long.")]
    public string? Empmidnm { get; set; }

    [Required]
    [Display(Name = "Suffix")]
    public string? Suffix { get; set; }

    [Required]
    [Display(Name = "Empalias")]
    [StringLength(15, ErrorMessage = "This field must be 15 long.")]
    public string? Empalias { get; set; }

    [Required]
    [Display(Name = "Client")]
    [StringLength(5, ErrorMessage = "This field must be 5 long.")]
    public string? Client { get; set; }

    [Required]
    [Display(Name = "Client_")]
    [StringLength(5, ErrorMessage = "This field must be 5 long.")]
    public string? Client_ { get; set; }

    [Required]
    [Display(Name = "Basicrate")]
    public string? Basicrate { get; set; }

    [Required]
    [Display(Name = "Paytype")]
    public string? Paytype { get; set; }

    [Required]
    [Display(Name = "Admin")]
    [StringLength(1, ErrorMessage = "This field must be 1 long.")]
    public string? Admin { get; set; }

    [Required]
    [Display(Name = "Cashbond")]
    public string? Cashbond { get; set; }

    [Required]
    [Display(Name = "Workdays")]
    public string? Workdays { get; set; }

    [Required]
    [Display(Name = "Allowrate")]
    public string? Allowrate { get; set; }

    [Required]
    [Display(Name = "Allowtype")]
    [StringLength(1, ErrorMessage = "This field must be 1 long.")]
    public string? Allowtype { get; set; }

    [Required]
    [Display(Name = "Allowfix")]
    [StringLength(1, ErrorMessage = "This field must be 1 long.")]
    public string? Allowfix { get; set; }

    [Required]
    [Display(Name = "Allow2rate")]
    public string? Allow2rate { get; set; }

    [Required]
    [Display(Name = "Allow2type")]
    [StringLength(1, ErrorMessage = "This field must be 1 long.")]
    public string? Allow2type { get; set; }

    [Required]
    [Display(Name = "Allow2fix")]
    [StringLength(1, ErrorMessage = "This field must be 1 long.")]
    public string? Allow2fix { get; set; }

    [Required]
    [Display(Name = "Allow3rate")]
    public string? Allow3rate { get; set; }

    [Required]
    [Display(Name = "Allow3type")]
    [StringLength(1, ErrorMessage = "This field must be 1 long.")]
    public string? Allow3type { get; set; }

    [Required]
    [Display(Name = "Allow3fix")]
    [StringLength(1, ErrorMessage = "This field must be 1 long.")]
    public string? Allow3fix { get; set; }

    [Required]
    [Display(Name = "Allow4rate")]
    public string? Allow4rate { get; set; }

    [Required]
    [Display(Name = "Allow4type")]
    [StringLength(1, ErrorMessage = "This field must be 1 long.")]
    public string? Allow4type { get; set; }

    [Required]
    [Display(Name = "Allow4fix")]
    [StringLength(1, ErrorMessage = "This field must be 1 long.")]
    public string? Allow4fix { get; set; }

    [Required]
    [Display(Name = "Movnumber")]
    [StringLength(10, ErrorMessage = "This field must be 10 long.")]
    public string? Movnumber { get; set; }

    [Required]
    [Display(Name = "Movmode")]
    [StringLength(1, ErrorMessage = "This field must be 1 long.")]
    public string? Movmode { get; set; }

    [Required]
    [Display(Name = "Movdate")]
    [DataType(DataType.Date)]
    public DateTime Movdate { get; set; }

    [Required]
    [Display(Name = "Movend")]
    [DataType(DataType.Date)]
    public DateTime Movend { get; set; }

    [Required]
    [Display(Name = "Dutydate")]
    [DataType(DataType.Date)]
    public DateTime Dutydate { get; set; }

    [Required]
    [Display(Name = "Addr1")]
    [StringLength(150, ErrorMessage = "This field must be 150 long.")]
    public string? Addr1 { get; set; }

    [Required]
    [Display(Name = "Mlacode_")]
    [StringLength(5, ErrorMessage = "This field must be 5 long.")]
    public string? Mlacode_ { get; set; }

    [Required]
    [Display(Name = "Tel1")]
    [StringLength(15, ErrorMessage = "This field must be 15 long.")]
    public string? Tel1 { get; set; }

    [Required]
    [Display(Name = "Addr2")]
    [StringLength(150, ErrorMessage = "This field must be 150 long.")]
    public string? Addr2 { get; set; }

    [Required]
    [Display(Name = "Procode_")]
    [StringLength(5, ErrorMessage = "This field must be 5 long.")]
    public string? Procode_ { get; set; }

    [Required]
    [Display(Name = "Tel2")]
    [StringLength(15, ErrorMessage = "This field must be 15 long.")]
    public string? Tel2 { get; set; }

    [Required]
    [Display(Name = "Date of Birth")]
    [DataType(DataType.Date)]
    public DateTime Empbirth { get; set; }

    [Required]
    [Display(Name = "Birth Place")]
    [StringLength(75, ErrorMessage = "This field must be 75 long.")]
    public string? Birthplace { get; set; }

    [Required]
    [Display(Name = "Gender")]
    [StringLength(1, ErrorMessage = "This field must be 1 long.")]
    public string? Sex_ { get; set; }

    [Required]
    [Display(Name = "Civil Status")]
    [StringLength(1, ErrorMessage = "This field must be 1 long.")]
    public string? Civstat_ { get; set; }

    [Required]
    [Display(Name = "Citizen")]
    [StringLength(15, ErrorMessage = "This field must be 15 long.")]
    public string? Citizen { get; set; }

    [Required]
    [Display(Name = "Height")]
    public string? Height { get; set; }

    [Required]
    [Display(Name = "Weight")]
    public string? Weight { get; set; }

    [Required]
    [Display(Name = "Tin")]
    [StringLength(11, ErrorMessage = "This field must be 11 long.")]
    public string? Tin { get; set; }

    [Required]
    [Display(Name = "Sss")]
    [StringLength(12, ErrorMessage = "This field must be 12 long.")]
    public string? Sss { get; set; }

    [Required]
    [Display(Name = "Hdmf")]
    [StringLength(12, ErrorMessage = "This field must be 12 long.")]
    public string? Hdmf { get; set; }

    [Required]
    [Display(Name = "Religion")]
    [StringLength(35, ErrorMessage = "This field must be 35 long.")]
    public string? Religion { get; set; }

    [Required]
    [Display(Name = "Hair")]
    [StringLength(5, ErrorMessage = "This field must be 5 long.")]
    public string? Hair { get; set; }

    [Required]
    [Display(Name = "Eye Color")]
    [StringLength(5, ErrorMessage = "This field must be 5 long.")]
    public string? Eyes { get; set; }

    [Required]
    [Display(Name = "Spouse")]
    [StringLength(25, ErrorMessage = "This field must be 25 long.")]
    public string? Spouse { get; set; }

    [Required]
    [Display(Name = "Occupation")]
    [StringLength(75, ErrorMessage = "This field must be 75 long.")]
    public string? Occupation { get; set; }

    [Required]
    [Display(Name = "Number of Children")]
    public string? Nochildren { get; set; }

    [Required]
    [Display(Name = "Datehired")]
    [DataType(DataType.Date)]
    public DateTime Datehired { get; set; }

    [Required]
    [Display(Name = "Separate")]
    [DataType(DataType.Date)]
    public DateTime Separate { get; set; }

    [Required]
    [Display(Name = "Position_")]
    [StringLength(5, ErrorMessage = "This field must be 5 long.")]
    public string? Position_ { get; set; }

    [Required]
    [Display(Name = "Status")]
    [StringLength(1, ErrorMessage = "This field must be 1 long.")]
    public string? Empstat_ { get; set; }

    [Required]
    [Display(Name = "Statusdate")]
    [DataType(DataType.Date)]
    public DateTime Statusdate { get; set; }

    [Required]
    [Display(Name = "Seclicense")]
    [StringLength(15, ErrorMessage = "This field must be 15 long.")]
    public string? Seclicense { get; set; }

    [Required]
    [Display(Name = "Licexpire")]
    [DataType(DataType.Date)]
    public DateTime Licexpire { get; set; }

    [Required]
    [Display(Name = "Trainat")]
    [StringLength(30, ErrorMessage = "This field must be 30 long.")]
    public string? Trainat { get; set; }

    [Required]
    [Display(Name = "Datetrain")]
    [DataType(DataType.Date)]
    public DateTime Datetrain { get; set; }

    [Required]
    [Display(Name = "Insurance")]
    [StringLength(20, ErrorMessage = "This field must be 20 long.")]
    public string? Insurance { get; set; }

    [Required]
    [Display(Name = "Policyno")]
    [StringLength(15, ErrorMessage = "This field must be 15 long.")]
    public string? Policyno { get; set; }

    [Required]
    [Display(Name = "Facevalue")]
    public string? Facevalue { get; set; }

    [Required]
    [Display(Name = "Premium")]
    public string? Premium { get; set; }

    [Required]
    [Display(Name = "Insexpire")]
    [DataType(DataType.Date)]
    public DateTime Insexpire { get; set; }

    [Required]
    [Display(Name = "Exmilitary")]
    [StringLength(1, ErrorMessage = "This field must be 1 long.")]
    public string? Exmilitary { get; set; }

    [Required]
    [Display(Name = "Csp")]
    [StringLength(1, ErrorMessage = "This field must be 1 long.")]
    public string? Csp { get; set; }

    [Required]
    [Display(Name = "Cpp")]
    [StringLength(1, ErrorMessage = "This field must be 1 long.")]
    public string? Cpp { get; set; }

    [Required]
    [Display(Name = "Rotc")]
    [StringLength(1, ErrorMessage = "This field must be 1 long.")]
    public string? Rotc { get; set; }

    [Required]
    [Display(Name = "Ellevel")]
    [StringLength(8, ErrorMessage = "This field must be 8 long.")]
    public string? Ellevel { get; set; }

    [Required]
    [Display(Name = "Hslevel")]
    [StringLength(8, ErrorMessage = "This field must be 8 long.")]
    public string? Hslevel { get; set; }

    [Required]
    [Display(Name = "College_")]
    [StringLength(8, ErrorMessage = "This field must be 8 long.")]
    public string? College_ { get; set; }

    [Required]
    [Display(Name = "Course")]
    [StringLength(25, ErrorMessage = "This field must be 25 long.")]
    public string? Course { get; set; }

    [Required]
    [Display(Name = "Volevel")]
    [StringLength(8, ErrorMessage = "This field must be 8 long.")]
    public string? Volevel { get; set; }

    [Required]
    [Display(Name = "Vocourse")]
    [StringLength(25, ErrorMessage = "This field must be 25 long.")]
    public string? Vocourse { get; set; }

    [Required]
    [Display(Name = "Language")]
    [StringLength(15, ErrorMessage = "This field must be 15 long.")]
    public string? Language { get; set; }

    [Required]
    [Display(Name = "Skill1")]
    [StringLength(2, ErrorMessage = "This field must be 2 long.")]
    public string? Skill1 { get; set; }

    [Required]
    [Display(Name = "Skill2")]
    [StringLength(2, ErrorMessage = "This field must be 2 long.")]
    public string? Skill2 { get; set; }

    [Required]
    [Display(Name = "Skill3")]
    [StringLength(2, ErrorMessage = "This field must be 2 long.")]
    public string? Skill3 { get; set; }

    [Required]
    [Display(Name = "Skill4")]
    [StringLength(2, ErrorMessage = "This field must be 2 long.")]
    public string? Skill4 { get; set; }

    [Required]
    [Display(Name = "Taxcode")]
    [StringLength(3, ErrorMessage = "This field must be 3 long.")]
    public string? Taxcode { get; set; }

    [Required]
    [Display(Name = "Acctcode")]
    [StringLength(21, ErrorMessage = "This field must be 21 long.")]
    public string? Acctcode { get; set; }

    [Required]
    [Display(Name = "Awol")]
    [StringLength(6, ErrorMessage = "This field must be 6 long.")]
    public string? Awol { get; set; }

    [Required]
    [Display(Name = "Dismiss")]
    [StringLength(6, ErrorMessage = "This field must be 6 long.")]
    public string? Dismiss { get; set; }

    [Required]
    [Display(Name = "Astart")]
    [DataType(DataType.Date)]
    public DateTime Astart { get; set; }

    [Required]
    [Display(Name = "Aend")]
    [DataType(DataType.Date)]
    public DateTime Aend { get; set; }

    [Required]
    [Display(Name = "Adays")]
    public string? Adays { get; set; }

    [Required]
    [Display(Name = "Dstart")]
    [DataType(DataType.Date)]
    public DateTime Dstart { get; set; }

    [Required]
    [Display(Name = "Dend")]
    [DataType(DataType.Date)]
    public DateTime Dend { get; set; }

    [Required]
    [Display(Name = "Ddays")]
    public string? Ddays { get; set; }

    [Required]
    [Display(Name = "Emrname")]
    [StringLength(25, ErrorMessage = "This field must be 25 long.")]
    public string? Emrname { get; set; }

    [Required]
    [Display(Name = "Emrtel")]
    [StringLength(15, ErrorMessage = "This field must be 15 long.")]
    public string? Emrtel { get; set; }

    [Required]
    [Display(Name = "Emraddr")]
    [StringLength(60, ErrorMessage = "This field must be 60 long.")]
    public string? Emraddr { get; set; }

    [Required]
    [Display(Name = "Guardexp")]
    public string? Guardexp { get; set; }

    [Required]
    [Display(Name = "Comtaxno")]
    [StringLength(10, ErrorMessage = "This field must be 10 long.")]
    public string? Comtaxno { get; set; }

    [Required]
    [Display(Name = "Comtaxdate")]
    [DataType(DataType.Date)]
    public DateTime Comtaxdate { get; set; }

    [Required]
    [Display(Name = "Comtax_at")]
    [StringLength(30, ErrorMessage = "This field must be 30 long.")]
    public string? Comtax_at { get; set; }

    [Required]
    [Display(Name = "Bloodtype")]
    [StringLength(10, ErrorMessage = "This field must be 10 long.")]
    public string? Bloodtype { get; set; }

    [Required]
    [Display(Name = "Marks")]
    [StringLength(60, ErrorMessage = "This field must be 60 long.")]
    public string? Marks { get; set; }

    [Required]
    [Display(Name = "Complexion")]
    [StringLength(10, ErrorMessage = "This field must be 10 long.")]
    public string? Complexion { get; set; }

    [Required]
    [Display(Name = "NBI Expire")]
    [DataType(DataType.Date)]
    public DateTime Exp_nbi { get; set; }

    [Required]
    [Display(Name = "Police Expire")]
    [DataType(DataType.Date)]
    public DateTime Exp_police { get; set; }

    [Required]
    [Display(Name = "PNP Expire")]
    [DataType(DataType.Date)]
    public DateTime Exp_pnp { get; set; }

    [Required]
    [Display(Name = "Barangay Expire")]
    [DataType(DataType.Date)]
    public DateTime Exp_brgy { get; set; }

    [Required]
    [Display(Name = "Court Expire")]
    [DataType(DataType.Date)]
    public DateTime Exp_court { get; set; }

    [Required]
    [Display(Name = "Neuro Expire")]
    [DataType(DataType.Date)]
    public DateTime Exp_neuro { get; set; }

    [Required]
    [Display(Name = "Drug Expire")]
    [DataType(DataType.Date)]
    public DateTime Exp_drug { get; set; }

    [Required]
    [Display(Name = "W_birthc")]
    [StringLength(1, ErrorMessage = "This field must be 1 long.")]
    public string? W_birthc { get; set; }

    [Required]
    [Display(Name = "W_closingr")]
    [StringLength(1, ErrorMessage = "This field must be 1 long.")]
    public string? W_closingr { get; set; }

    [Required]
    [Display(Name = "W_trncert")]
    [StringLength(1, ErrorMessage = "This field must be 1 long.")]
    public string? W_trncert { get; set; }

    [Required]
    [Display(Name = "W_prelic")]
    [StringLength(1, ErrorMessage = "This field must be 1 long.")]
    public string? W_prelic { get; set; }

    [Required]
    [Display(Name = "W_certemp")]
    [StringLength(1, ErrorMessage = "This field must be 1 long.")]
    public string? W_certemp { get; set; }

    [Required]
    [Display(Name = "W_medexam")]
    [StringLength(1, ErrorMessage = "This field must be 1 long.")]
    public string? W_medexam { get; set; }

    [Required]
    [Display(Name = "Gkerate")]
    public string? Gkerate { get; set; }

    [Required]
    [Display(Name = "Clname")]
    [StringLength(254, ErrorMessage = "This field must be 254 long.")]
    public string? Clname { get; set; }

    [Required]
    [Display(Name = "Mlaname")]
    [StringLength(20, ErrorMessage = "This field must be 20 long.")]
    public string? Mlaname { get; set; }

    [Required]
    [Display(Name = "Age")]
    public string? Age { get; set; }

    [Required]
    [Display(Name = "Mbranch")]
    [StringLength(20, ErrorMessage = "This field must be 20 long.")]
    public string? Mbranch { get; set; }

    [Required]
    [Display(Name = "Myear")]
    [StringLength(20, ErrorMessage = "This field must be 20 long.")]
    public string? Myear { get; set; }

    [Required]
    [Display(Name = "Mnature")]
    [StringLength(20, ErrorMessage = "This field must be 20 long.")]
    public string? Mnature { get; set; }

    [Required]
    [Display(Name = "Remarks")]
    [StringLength(150, ErrorMessage = "This field must be 150 long.")]
    public string? Remarks { get; set; }

    [Required]
    [Display(Name = "Badgeno")]
    [StringLength(10, ErrorMessage = "This field must be 10 long.")]
    public string? Badgeno { get; set; }

    [Required]
    [Display(Name = "Guardnoyrs")]
    [StringLength(5, ErrorMessage = "This field must be 5 long.")]
    public string? Guardnoyrs { get; set; }

    [Required]
    [Display(Name = "Militarynoyr")]
    [StringLength(5, ErrorMessage = "This field must be 5 long.")]
    public string? Militarynoyr { get; set; }

    [Required]
    [Display(Name = "Pagibigno")]
    [StringLength(15, ErrorMessage = "This field must be 15 long.")]
    public string? Pagibigno { get; set; }

    [Required]
    [Display(Name = "Phic")]
    [StringLength(15, ErrorMessage = "This field must be 15 long.")]
    public string? Phic { get; set; }

    [Required]
    [Display(Name = "Bank")]
    [StringLength(10, ErrorMessage = "This field must be 10 long.")]
    public string? Bank { get; set; }

    [Required]
    [Display(Name = "Medical Expire")]
    [DataType(DataType.Date)]
    public DateTime Expmed { get; set; }

    [Required]
    [Display(Name = "Regref")]
    [DataType(DataType.Date)]
    public DateTime Regref { get; set; }

    [Required]
    [Display(Name = "Empbasicrate")]
    public string? Empbasicrate { get; set; }

    [Required]
    [Display(Name = "Rateid")]
    public string? Rateid { get; set; }

    [Required]
    [Display(Name = "Empecola")]
    public string? Empecola { get; set; }

    [Required]
    [Display(Name = "Xmark")]
    public string? Xmark { get; set; }

    [Required]
    [Display(Name = "Suretybondquota")]
    public string? Suretybondquota { get; set; }

    [Required]
    [Display(Name = "Drv_license")]
    [StringLength(25, ErrorMessage = "This field must be 25 long.")]
    public string? Drv_license { get; set; }

    [Required]
    [Display(Name = "Drv_exp")]
    [DataType(DataType.Date)]
    public DateTime Drv_exp { get; set; }

    [Required]
    [Display(Name = "Istaxable")]
    public string? Istaxable { get; set; }

    [Required]
    [Display(Name = "Isconfi")]
    public string? Isconfi { get; set; }

    [Required]
    [Display(Name = "Iswithsss")]
    public string? Iswithsss { get; set; }

    [Required]
    [Display(Name = "Iswithgsis")]
    public string? Iswithgsis { get; set; }

    [Required]
    [Display(Name = "Iswithphic")]
    public string? Iswithphic { get; set; }

    [Required]
    [Display(Name = "Iswithpagibig")]
    public string? Iswithpagibig { get; set; }

    [Required]
    [Display(Name = "Ismaxsss")]
    public string? Ismaxsss { get; set; }

    [Required]
    [Display(Name = "Sgcode")]
    public string? Sgcode { get; set; }

    [Required]
    [Display(Name = "Dpadate")]
    [DataType(DataType.Date)]
    public DateTime Dpadate { get; set; }

    [Required]
    [Display(Name = "Dpclient")]
    public string? Dpclient { get; set; }

    [Required]
    [Display(Name = "Email")]
    [StringLength(45, ErrorMessage = "This field must be 45 long.")]
    public string? Email { get; set; }

    [Required]
    [Display(Name = "Passwd")]
    [StringLength(60, ErrorMessage = "This field must be 60 long.")]
    public string? Passwd { get; set; }
}