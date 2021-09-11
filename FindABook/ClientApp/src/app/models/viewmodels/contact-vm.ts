export class JwtTokenVM
{
    constructor(
        public ContactId : string =null,
        public sub : string =null,
        public jti : string =null,
        public email : string =null,
        public unique_name : string =null,
        public AdditionalInfo : string =null,
        public nbf : number =null,
        public exp : number =null,
        public iat : number =null,
    ){}
}

export class ContactDetailsVM{
     constructor(public MobileNumber: string=null,
       public TelephoneNumber: string=null,
       public FaxNumber: string=null,
       public EmailAddress: string= null){}
}

export class SettingsVM{
   constructor(public isActive: boolean=null,
    public isDeleted: boolean=null,
    public registrationDate:Date= null,
    public joinedDate: Date=null){}
}

export class AddressVM{
  constructor(public FullAddress:string=null,
    public AddressNumber:string= null,
    public AddressName: string=null,
    public AddressCode: string= null,
    public City:string =null,
    public Country:string=null,
    public PostalFullAddress:string=null,
    public PostalAddressLine1: string= null,
    public PostalAddressLine2: string= null,
    public PostalCode: string=null,
    public Latitude:number=null,
    public Longitude:number=null){}
}

export class ProfileVM{

}
