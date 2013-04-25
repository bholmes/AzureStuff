using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TodoServerLibrary
{
    public class LocalSqlTodoList : SqlTodoListBase
    {
        TodoDataContext _dataContext = new TodoDataContext();

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
