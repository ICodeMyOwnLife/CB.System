﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Input;


namespace CB.Model.Common
{
    public abstract class ViewModelBase: ObservableObject
    {
        #region Fields
        private double _progress;
        private string _state;
        protected readonly SynchronizationContext _synchronizationContext = SynchronizationContext.Current;
        #endregion


        #region  Properties & Indexers
        public virtual double Progress
        {
            get { return _progress; }
            protected set { SetPropertySync(ref _progress, value); }
        }

        public virtual string State
        {
            get { return _state; }
            protected set { SetProperty(ref _state, value); }
        }
        #endregion


        #region Implementation
        protected virtual ICommand GetCommand(ref ICommand command, Action<object> execute,
            Predicate<object> canExecute = null)
            => command ?? (command = new RelayCommand(execute, canExecute));

        protected virtual bool SetFieldSync<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;

            field = value;

            if (_synchronizationContext != null)
            {
                _synchronizationContext.Send(_ => NotifyPropertyChanged(propertyName), null);
            }
            else
            {
                NotifyPropertyChanged(propertyName);
            }

            return true;
        }

        protected virtual bool SetPropertySync<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            return SetFieldSync(ref field, value, propertyName);
        }
        #endregion
    }
}