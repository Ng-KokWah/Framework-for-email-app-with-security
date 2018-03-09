# Framework-for-email-app-with-security

V1.0.0
This is a rough framework for an email app, and also a testing of a concept
It has Security again OWSAP Top 10, Cross-Site-Scripting (XSS) & SQL Injection.
This is done mostly by Input Validation on the client side and Server side Input Sanitization using HtmlSanitizer by Michael Ganss
and also a blacklist of disallowed inputs

For the email login you just need to use your email login

After downloading the project:
1) Double click on the  EmailApp.sln file to open the project in Visual Studio
2) Navigate to Views > Email > EmailLogin.cshtml
3) Run the project and login using your email login credentials (this is needed so that you can be authorized to send the email in the next page)
4) Click on the login button
5) Type what you want to send as well as the recipient email and click on send

Note: This is just a proof of concept and a very basic framework for an email app
