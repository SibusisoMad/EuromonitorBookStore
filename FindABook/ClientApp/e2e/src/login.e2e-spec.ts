import { LoginPage } from './login.po';
describe('Login tests', () => {
    let page: LoginPage;
    beforeEach(() => {
        page = new LoginPage();
        page.navigateTo();        
    });

    it('Login form should be valid', () => {
        page.getEmailTextbox().sendKeys('Sibusiso');
        page.getPasswordTextbox().sendKeys('Test@1234');
       
        let form = page.navigateTo()
        expect(form).toContain('ng-valid');
       });
});