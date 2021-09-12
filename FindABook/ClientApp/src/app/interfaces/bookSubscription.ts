import { Book } from "./book";

export class BookSubscription{
    constructor(
     BookId : number,
     UserId : string= "",
     SubscriptionId : number=null,
     Subscribed : boolean=false,
     LastModified : Date=null,
     Book : Book=null){}
}