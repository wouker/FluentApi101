using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace UnitTestProject_FluentApi101
{
    public sealed class Person : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _firstName;
        private string _lastName;
        private DateTime _birthDate;

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(() => FirstName);
                OnPropertyChanged(() => FullName);
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(() => LastName);
                OnPropertyChanged(() => FullName);
            }
        }

        public string FullName => $"{FirstName} {LastName}";

        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                OnPropertyChanged(() => BirthDate);
            }
        }

        private void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            if (PropertyChanged == null)
                return;

            var propertyName = ((MemberExpression)propertyExpression.Body)
                .Member.Name;

            var args = new PropertyChangedEventArgs(propertyName);

            PropertyChanged(this, args);
        }
    }

}
