using System.Collections.Generic;

namespace Elders.Ares
{
    public static class PropertyFactory
    {
        public static IProperty<T> AsProperty<T>(T value)
        {
            return new PropertyDefault<T>(value);
        }
        public static IProperty<T> AsProperty<T>(T? value, T defaultValue) where T : struct
        {
            return new PropertyDefault<T>(value.HasValue ? value.Value : defaultValue);
        }
        public static IProperty<T> AsProperty<T>(IProperty<T> value, T defaultValue) where T : class
        {
            return new PropertyWrapperProperty<T>(value, defaultValue);
        }
        public static IProperty<T> AsProperty<T>(params IProperty<T>[] values) where T : class
        {
            return AsProperty((IEnumerable<IProperty<T>>)values);
        }
        public static IProperty<T> AsProperty<T>(IEnumerable<IProperty<T>> values) where T : class
        {
            return new ChainedProperty<T>(values);
        }
        public static IProperty<T> NullProperty<T>() where T : class
        {
            return new DefaultProperty<T>();
        }

        private class PropertyDefault<T> : IProperty<T>
        {
            private T value;

            public PropertyDefault(T value)
            {
                this.value = value;
            }

            public T Get()
            {
                return this.value;
            }
        }
        private class PropertyWrapperProperty<T> : IProperty<T> where T : class
        {
            private IProperty<T> value;
            private T defaultValue;

            public PropertyWrapperProperty(IProperty<T> value, T defaultValue)
            {
                this.value = value;
                this.defaultValue = defaultValue;
            }

            public T Get()
            {
                return this.value.Get() ?? this.defaultValue;
            }
        }
        private class ChainedProperty<T> : IProperty<T> where T : class
        {
            private IEnumerable<IProperty<T>> values;

            public ChainedProperty(IEnumerable<IProperty<T>> values)
            {
                this.values = values;
            }

            public T Get()
            {
                foreach (IProperty<T> value in this.values)
                {
                    T v = value.Get();
                    if (v != null)
                    {
                        return v;
                    }
                }
                return null;
            }
        }
        private class DefaultProperty<T> : IProperty<T> where T : class
        {
            public T Get()
            {
                return default(T);
            }
        }
    }
}
