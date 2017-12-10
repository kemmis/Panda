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

### Configuration
As of Panda version 1.1, the only configuration that is necessary is to set the database connection string for the application. You will find the `DefaultConnection` connection string defined inside the `appsettings.json` file. You will need to configure this connection string to point to the SqlServer and database that you wish to use. When Panda starts for the first time, it will create the tables and seed the data that it needs to run. No need to manually execute any sql scripts to install Panda.

### Running Panda Locally
Running Panda locally requires that you have the .NET Core 2 SDK, Node.js, and some flavor of SQL Server installed. Once you have these dependencies installed, you're ready to run Panda.

#### Running Panda From Binaries via the dotnet Command Line
1. Download the Panda binaries for latest version and unzip them to your desired directory.
2. Open your favorite command line and navigate to the directory where you unzipped the Panda binaries.
3. Run `dotnet panda.web.dll`

#### Running Panda From Source via the dotnet Command Line
1. Acquired the Panda source by either cloning the git repository or downloading the source to your computer.
2. Open your favorite command line and navigate tot he directory where the Panda source files live.
3. Change directories to the `/src/Panda.Web/` folder.
4. Run `npm install`
5. Run `dotnet run`

#### Running Panda From Visual Studio
1. Acquired the Panda source by either cloning the git repository or downloading the source to your computer.
2. Open Panda.sln
3. Make sure the Panda.Web project is selected in the solution explorer pane, and hit F5 to start the app with debugging.

#### Manually Building Webpack (Helpful for debugging Webpack issues)
- Run `webpack --config .\webpack.config.vendor.js`
- Run `webpack --config .\webpack.config.js`

### Problems & Solutions

#### *Module not found: Error: Can't resolve './$$_gendir/app/app.module.ngfactory'* when running webpack
There's a conflict with more recent versions of the `enhanced-resolve` package dependency. One solution is to 'pin' the version of `enhanced-resolve` to version `3.3.0`. More information [here](https://github.com/angular/angular-cli/issues/4551#issuecomment-322047088). As of this writing, `enhanced-resolve` should be pinned to this version in both the npm-shrinkwrap.json file and the yarn.lock file.

#### Azure deploy fails with message *Thread was being aborted*.
This happens when the deploy commands take too long to execute. You can resolve this by setting the `SCM_COMMAND_IDLE_TIMEOUT` app setting to `900` (900 seconds == 15 minutes) for your app in the Azure portal. More information [here](https://github.com/projectkudu/kudu/issues/2089#issuecomment-262499421).











