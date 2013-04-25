using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TodoServerLibrary
{
    public class AzureSqlTodoList : SqlTodoListBase
    {
        TodoDataContext _dataContext = new TodoDataContext("Data Source=tcp:juyq0y51g1.database.windows.net,1433;Initial Catalog=mobilltest_db;User ID=bill@juyq0y51g1;Password='6/\"Q[8Eiee)y|!I^8Xox'");

        protected override TodoDataContext DataContext
        {
            get { return _dataContext; }
        }

        public override void Dispose()
        {
            if (_dataContext != null)
                _dataContext.Dispose();

            _dataContext = null;
        }

    }
}
