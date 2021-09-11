import { Category } from "./category";

export class Book
{
    constructor(
        public Title :string,
        public Description :string,
        public Price :number,
        public CategoryId :number,
        public BestSeller :boolean,
        public Publisher : string,
        public ImageUrl: string, 
        public BookId?:number,   
        public Category?: Category,
        public BookSubscription? :any []
    ){

    }
    
}
