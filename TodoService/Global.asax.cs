using System;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.WebHost.Endpoints;
using Funq;
using TodoLibrary;
using System.Linq;
using TodoServerLibrary;

namespace TodoService
{
    [Route("/todolist")]
    public class TodoListRequest
    {
        
    }

    [Route("/markitemcomplete")]
    [Route("/markitemcomplete/{Id}")]
    public class MarkItemCompleteRequest
    {
        public int Id { get; set; }
    }

    [Route("/additem")]
    [Route("/additem/{Title}")]
    public class AddItemRequest
    {
        public string Title { get; set; }
    }

    public class TodoService : Service
    {
        private object CreateListAndCall(Func<ITodoList, object> func)
        {
            ITodoList list = null;
            try
            {
                list = new AzureSqlTodoList();

                return func(list);
            }

            finally
            {
                if (list != null && list is IDisposable)
                {
                    var dispose = list as IDisposable;
                    dispose.Dispose();
                }
            }
        }
        
        public object Get(TodoListRequest request)
        {
            return CreateListAndCall(list =>
            {
                return list.Items.ToList();
            });
        }

        public void Put(MarkItemCompleteRequest request)
        {
            CreateListAndCall(list =>
            {
                list.MarkComplete(new TodoItem { Id = request.Id });
                return null;
            });
        }

        public void Post(AddItemRequest request)
        {
            CreateListAndCall(list =>
            {
                list.AddItem(request.Title);
                return null;
            });
        }
    }

    public class Global : System.Web.HttpApplication
    {
        public class HelloAppHost : AppHostBase
        {
            //Tell Service Stack the name of your application and where to find your web services
            public HelloAppHost() : base("Todp Web Services", typeof(Global).Assembly) { }

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