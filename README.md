# Panda
Panda is a blogging system that runs on ASP.NET Core 2, Angular 4, and SqlServer. 

### Hosting Requirements
Panda requires .NET Core 2 and Node.js. We highly recommend using Microsoft's Azure web application offering. There's a free pricing tier so you can try things out. Then it's less than $10 a month for the pricing tier which allows you to attach a custom domain. 

Panda does not support multiple instances or container hosting at this time, so you should choose to "scale up" rather than "scale out" if you need to handle more traffic to your blog. Support for container hosting is coming soon!

### Setup & Deployment
If you're hosting on Azure, we recommend auto deploying your blog files from GitHub.
1. Fork Panda on GitHub
2. In Azure Portal, add a "Web App + SQL" instance. Pay attention to the service plans / pricing tiers. You probably want to go with the cheapest. You can always change this later.
3. Under "Application Settings" in the Azure portal, set your app to use a recent version of Node.js by adding the `WEBSITE_NODE_DEFAULT_VERSION` setting and setting its value to `8.1.4`.
4. Set the `SCM_COMMAND_IDLE_TIMEOUT` setting to a value of `900`. This keeps the deployment from timing out since it can take a while for the app to build and deploy.
5. (Optional) If you happen to be deploying from a branch other than master, then you'll want to add the `SCM_USE_LIBGIT2SHARP_REPOSITORY` setting with a value of `0` (zero). This avoids a bug that currently exists with libgit2sharp. 
6. Under "Deployment Options" in the Azure portal, configure the deployment source to use  your new fork of Panda on GitHub.














