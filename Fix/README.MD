
Web view:

run Fix.WebApp

if db prolem: setup ur db connection string in Fix.WebApp/appsettings.json

the project have 1 unit and 1 integration test, they are in the test folder

Unit test checks /Home/Index page

Integration test checks that page is available

to enter web nodes administration panel: just click button in the header.

There are auth required. Just register and continue

Also we have '/hangfire' page. U can see recurrent tasks: 1 for tasks management, others - concrete web nodes checkers (name are equal web nodes tasks id)
