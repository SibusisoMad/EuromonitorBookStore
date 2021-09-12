export const environment = {
  production: false,
  APIUrl: "https://localhost:44308/",
  baseAPIUrl: "https://localhost:44308/api/"
};

export const ApiRoutes = {
  baseBookUrl:"api/BooksApi/",
  loginApiUrl:environment.baseAPIUrl+"/UserAuth/Login",
  registerApiUrl:environment.baseAPIUrl+"api/UserAuth/Register",
  purchaseBookUrl:environment.baseAPIUrl+"/BookSubscriptionsApi",
  BookSubscriptionAPI:"api/BookSubscriptionsApi"
};