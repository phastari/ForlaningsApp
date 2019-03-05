using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Interactivity;

namespace FiefApp.Common.Infrastructure.CustomCommands
{
    /// <summary>
    /// Sets a specified property to a value when invoked.
    /// </summary>
    public class CustomSetterAction : TargetedTriggerAction<FrameworkElement>
    {
        #region Properties

        #region PropertyName

        /// <summary>
        /// Property that is being set by this setter.
        /// </summary>
        public string PropertyName
        {
            get => (string)GetValue(PropertyNameProperty);
            set => SetValue(PropertyNameProperty, value);
        }

        public static readonly DependencyProperty PropertyNameProperty =
            DependencyProperty.Register(
                "PropertyName", 
                typeof(string), 
                typeof(CustomSetterAction),
                new PropertyMetadata(String.Empty)
            );

        #endregion

        #region Value

        /// <summary>
        /// Property value that is being set by this setter.
        /// </summary>
        public object Value
        {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value", 
                typeof(object), 
                typeof(CustomSetterAction),
                new PropertyMetadata(null)
            );

        #endregion

        #endregion

        #region Overrides

        protected override void Invoke(object parameter)
        {
            var target = TargetObject ?? AssociatedObject;

            var targetType = target.GetType();

            var property = targetType.GetProperty(PropertyName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            if (property == null)
            {
                throw new ArgumentException($"Property not found: {PropertyName}");
            }

            if (property.CanWrite == false)
            {
                throw new ArgumentException($"Property is not settable: {PropertyName}");
            }

            object convertedValue;

            if (Value == null)
            {
                convertedValue = null;
            }
            else
            {
                var valueType = Value.GetType();
                var propertyType = property.PropertyType;

                if (valueType == propertyType)
                {
                    convertedValue = Value;
                }
                else
                {
                    var propertyConverter = TypeDescriptor.GetConverter(propertyType);

                    if (propertyConverter.CanConvertFrom(valueType))
                    {
                        convertedValue = propertyConverter.ConvertFrom(Value);
                    }
                    else if (valueType.IsSubclassOf(propertyType))
                    {
                        convertedValue = Value;
                    }
                    else
                    {
                        throw new ArgumentException($"Cannot convert type '{valueType}' to '{propertyType}'.");
                    }
                }
            }

            property.SetValue(target, convertedValue);
        }

        #endregion
    }
}