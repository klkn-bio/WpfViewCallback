using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace WpfViewCallback.ViewCommands
{
    public class ViewCommand : ObservableCollection<ViewCommandBinding>
    {
        private DependencyObject _attachedObject;
        private object _dataContext;
        #region DependencyProperty
        public static DependencyProperty CommandsProperty = DependencyProperty.RegisterAttached("Commands"
            , typeof(ViewCommand)
            , typeof(ViewCommand),
            new FrameworkPropertyMetadata(PropertyChangedCallback){});
        
        public static void SetCommands(DependencyObject element, ViewCommand value)
        {
            element.SetValue(CommandsProperty, value);
        }
        public static ViewCommand GetCommands(DependencyObject element)
        {
            if (element.GetValue(CommandsProperty) is ViewCommand result)
                return result;
            result = new ViewCommand();
            element.SetValue(CommandsProperty, result);
            return result;
        }

        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is ViewCommand oldValue)
                oldValue.Detach();
            if (e.NewValue is ViewCommand newValue)
                newValue.Attach(d);
        }

        #endregion DependencyProperty

        private void Detach()
        {
            if (_attachedObject == null)
                return;
            if (_dataContext != null)
            {
                foreach (var viewCommandBinding in Items)
                {
                    viewCommandBinding.Detach(_attachedObject, _dataContext);
                }
            }
            _dataContext = null;
            if (_attachedObject is FrameworkContentElement contentElement)
            {
                contentElement.DataContextChanged -= AttachedObjectOnDataContextChanged;
            }
            if (_attachedObject is FrameworkElement frameWorkElement)
            {
                frameWorkElement.DataContextChanged -= AttachedObjectOnDataContextChanged;
            }
            _attachedObject = null;
        }

        private void Attach(DependencyObject d)
        {
            if (_attachedObject != null)
                Detach();
            _attachedObject = d;
            if (_attachedObject is FrameworkContentElement contentElement)
            {
                contentElement.DataContextChanged += AttachedObjectOnDataContextChanged;
                _dataContext = contentElement.DataContext;
            }
            if (_attachedObject is FrameworkElement frameWorkElement)
            {
                frameWorkElement.DataContextChanged += AttachedObjectOnDataContextChanged;
                _dataContext = frameWorkElement.DataContext;
            }
            if (_dataContext != null)
            {
                foreach (var viewCommandBinding in Items)
                {
                    viewCommandBinding.Attach(_attachedObject, _dataContext);
                }
            }
        }

        private void AttachedObjectOnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.Property != FrameworkElement.DataContextProperty)
                return;
            if (_dataContext == e.NewValue)
                return;
            if (_dataContext != null)
            {
                foreach (var viewCommandBinding in Items)
                {
                    viewCommandBinding.Detach(_attachedObject, _dataContext);
                }
            }
            _dataContext = e.NewValue;
            if (_dataContext != null)
            {
                foreach (var viewCommandBinding in Items)
                {
                    viewCommandBinding.Attach(_attachedObject, _dataContext);
                }
            }
        }

        #region Overrides of ObservableCollection<ViewCommandBinding>

        /// <inheritdoc />
        protected override void InsertItem(int index, ViewCommandBinding item)
        {
            if(item == null)
                throw new ArgumentNullException(nameof(item));
            base.InsertItem(index, item);
            if (_attachedObject != null && _dataContext != null)
                item.Attach(_attachedObject, _dataContext);
        }

        /// <inheritdoc />
        protected override void RemoveItem(int index)
        {
            var item = Items[index];
            base.RemoveItem(index);
            if (_attachedObject != null && _dataContext != null)
                item.Detach(_attachedObject, _dataContext);
        }

        #endregion
    }
}
