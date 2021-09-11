export class RegisterVM {
    constructor(
    public Username :string,
    public FirstName:string,
    public LastName:string,
    public Password:string,
    public PasswordConfirm:string,
    public EmailAddress :string="",
    public MobileNumber :string="",
    public UsernameFromEmailOrMobile:boolean=true){}
}
