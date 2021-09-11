export const environment = {
  production: false,
  APIUrl: "https://localhost:44308/",
  baseAPIUrl: "https://localhost:44308/api/"
};

export const ApiRoutes = {
  baseBookUrl: environment.baseAPIUrl+"BooksApi/",
  loginApiUrl:environment.baseAPIUrl+"UserAuth/Login",
  registerApiUrl:environment.baseAPIUrl+"UserAuth/Register",
  
  purchaseBookUrl:""

};