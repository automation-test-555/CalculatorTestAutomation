Send-MailMessage `
 -From "automation.test.ci.reports@gmail.com" `
 -To "automation.test.ci.reports@gmail.com" `
 -Subject "Automation Test Report" `
 -Body "Allure report attached" `
 -SmtpServer "smtp.gmail.com" `
 -Port 587 `
 -UseSsl `
 -Credential (Get-Credential) `
 -Attachments "./allure-report/index.html"