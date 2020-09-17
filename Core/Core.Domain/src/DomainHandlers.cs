using System.Collections.Generic;
using System.Linq;
using System;

namespace Core.Domain
{
    public static class DomainHandlers
    {
        private static List<Type> _handlers = new List<Type>();

        public static void Register(Type handler)
        {
            _handlers.Add(handler);
        }

        public static void DispatchEvent(DomainEvent domainEvent)
        {
            foreach (Type handlerType in _handlers)
            {
                bool canHandleEvent = handlerType.GetInterfaces()
                    .Any(x => x.IsGenericType
                        && x.GetGenericTypeDefinition() == typeof(IHandler<>)
                        && x.GenericTypeArguments[0] == domainEvent.GetType());

                if (canHandleEvent)
                {
                    dynamic handler = Activator.CreateInstance(handlerType);
                    handler.Handle((dynamic)domainEvent);
                }
            }
        }
    }
}