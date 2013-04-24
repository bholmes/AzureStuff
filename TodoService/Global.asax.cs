using System;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.WebHost.Endpoints;
using Funq;


namespace TodoService
{
    [Route("/hello")]
    [Route("/hello/{Name}")]
    public class Hello
    {
        public string Name { get; set; }
    }

    public class HelloResponse
    {
        public string Result { get; set; }
    }

    public class HelloService : Service
    {
        public object Get(Hello request)
        {
            var retString = string.Format("Hello {0}", request.Name);

            return new HelloResponse { Result = retString };
        }
    }

    public class Global : System.Web.HttpApplication
    {
        public class HelloAppHost : AppHostBase
        {
            //Tell Service Stack the name of your application and where to find your web services
            public HelloAppHost() : base("Hello Web Services", typeof(HelloService).Assembly) { }

            public override void Configure(Container container)
            {
                SetConfig(new EndpointHostConfig { ServiceStackHandlerFactoryPath = "servicestack" });
            }
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            new HelloAppHost().Init();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}