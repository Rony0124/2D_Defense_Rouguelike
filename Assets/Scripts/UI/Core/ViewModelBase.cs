using UnityEngine;

namespace UI.Core
{
    public abstract class ViewModelBase : MonoBehaviour
    {
        protected ViewBase view;
        protected ModelBase model;
        
        public virtual void Awake()
        {
            view = GetComponent<ViewBase>();
            model = GetComponent<ModelBase>();
        }
    }
}
