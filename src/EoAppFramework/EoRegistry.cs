using System;
using System.Collections.Generic;
using SiaConsulting.EO.Abstractions;


namespace SiaConsulting.EO
{
    public static class EoRegistry
    {
        private static readonly Dictionary<Type, List<Pump>> _commandPumps = new Dictionary<Type, List<Pump>>();
        private static readonly Dictionary<Type, List<Pump>> _notificationPumps = new Dictionary<Type, List<Pump>>();
        private static readonly Dictionary<Type, List<Pump>> _queryPumps = new Dictionary<Type, List<Pump>>();
        private static readonly Dictionary<Type, List<Pump>> _eventPumps = new Dictionary<Type, List<Pump>>();
        public static void RegisterCommandPump<TCommand, TProcessor>(Pump<TProcessor> pump) where TCommand: ICommand where TProcessor: IProcessor
        {
            SafeAdd<TCommand>(_commandPumps, (Pump)pump);
        }

        public static void RegisterNotificationPump<TCommand, TProcessor>(Pump<TProcessor> pump) where TCommand: INotification where TProcessor : IProcessor
        {
            SafeAdd<TCommand>(_notificationPumps, (Pump)pump);
        }

        public static void RegisterQueryPump<TCommand, TProcessor>(Pump<TProcessor> pump) where TCommand: IQuery where TProcessor: IProcessor
        {
            SafeAdd<TCommand>(_queryPumps, (Pump)pump);
        }

        public static void RegisterEventPump<TCommand, TProcessor>(Pump<TProcessor> pump) where TCommand : IEvent where TProcessor : IProcessor
        {
            SafeAdd<TCommand>(_eventPumps, (Pump)pump);
        }

        public static List<Pump> GetCommandPumps<TCommand>(TCommand command) where TCommand : ICommand
        { 
            if(_commandPumps.ContainsKey(command.GetType()))
            {
                return _commandPumps[command.GetType()]; 
            }
            return new List<Pump>();
        }

        public static List<Pump> GetNotificationPumps<TNotification>(TNotification notification) where TNotification : INotification
        { 
            if(_notificationPumps.ContainsKey(notification.GetType()))
            {
                return _notificationPumps[notification.GetType()]; 
            }
            return new List<Pump>();
        }

        public static List<Pump> GetQueryPumps<TQuery>(TQuery query) where TQuery: IQuery 
        { 
            if(_queryPumps.ContainsKey(query.GetType()))
            {
                return _queryPumps[query.GetType()]; 
            }
            return new List<Pump>();
        }

        public static List<Pump> GetEventPumps<TEvent>(TEvent @event) where TEvent: IEvent 
        { 
            if(_eventPumps.ContainsKey(@event.GetType()))
            {
                return _eventPumps[@event.GetType()]; 
            }
            return new List<Pump>();
        }

        private static void SafeAdd<T>(Dictionary<Type, List<Pump>> dict, Pump value)
        {
            if(!dict.ContainsKey(typeof(T))) 
            {
                dict.Add(typeof(T), new List<Pump>());
            }
            dict[typeof(T)].Add(value);
        }
    }
}