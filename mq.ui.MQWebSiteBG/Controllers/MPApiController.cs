using match.application.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using mq.application.common;

namespace match.ui.institutioncenter
{
    public class MPApiController : ApiController
    {
        protected string SessionDomain = CommonHelper.GetConfigValue("SessionDomain");
        public IList<IDisposable> DisposableObjects { get; private set; }
        public MPApiController()
        {
            this.DisposableObjects = new List<IDisposable>();
        }
        protected void AddDisposableObject(object obj)
        {
            IDisposable disposable = obj as IDisposable;
            if (null != disposable)
            {
                this.DisposableObjects.Add(disposable);
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (IDisposable obj in this.DisposableObjects)
                {
                    if (null != obj)
                    {
                        obj.Dispose();
                    }
                }
            }
            base.Dispose(disposing);
        }

    }
}