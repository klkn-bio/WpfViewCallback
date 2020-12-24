using System;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace WpfViewCallback.ViewCommands
{
    /// <summary>
    /// Represent Binding
    /// </summary>
    public class ViewCommandBinding
    {
        private Delegate _attachedDelegate;
        private MethodInfo _removeMethod;
        /// <summary>
        /// Name of View Method
        /// </summary>
        public string ViewMethod { get; set; }

        /// <summary>
        /// Name of Event in View
        /// </summary>
        public string EventName { get; set; }

        internal void Attach(DependencyObject attachedObject, object dataContext)
        {
            var events = dataContext.GetType()
                .GetEvents(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
                .Union(dataContext.GetType().GetEvents(BindingFlags.Instance | BindingFlags.NonPublic))
                .ToArray();
            var eventInfo = events
                .FirstOrDefault(e => e.Name == EventName);
            if (eventInfo == null)
                throw new ArgumentNullException(nameof(EventName));
            var invokeMethod = eventInfo.EventHandlerType.GetMethod("Invoke");
            var mappedMethod = attachedObject.GetType()
                .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
                .Union(attachedObject.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic))
                .FirstOrDefault(m => m.Name == ViewMethod
                                     && m.ReturnType == invokeMethod.ReturnType
                                     && m.GetParameters().Length == invokeMethod.GetParameters().Length
                                     && m.GetParameters().Select((p, i) => new {p, i})
                                         .All(p => p.p.ParameterType == invokeMethod.GetParameters()[p.i].ParameterType)
                );
            if (mappedMethod == null)
                return;
            var attachedDelegate = Delegate.CreateDelegate(eventInfo.EventHandlerType, attachedObject, mappedMethod);
            eventInfo.AddMethod.Invoke(dataContext, new object[] { attachedDelegate });
            _attachedDelegate = attachedDelegate;
            _removeMethod = eventInfo.RemoveMethod;
        }

        internal void Detach(DependencyObject attachedObject, object dataContext)
        {
            if (_attachedDelegate == null)
                return;
            _removeMethod.Invoke(dataContext, new object[] {_attachedDelegate});
            _attachedDelegate = null;
        }
    }
}
