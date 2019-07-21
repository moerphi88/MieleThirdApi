using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MieleThirdApi.Model
{
    public class DetailViewCellModel : INotifyPropertyChanged
    {
        public DetailViewCellModel()
        {
            KeyText = "";
            ValueText = "";
            ImageSource = "";
            IsEditable = false;
        }

        private string _imageSource;
        private string _keyText;
        private string _valueText;
        private bool _isEditable;

        public string ImageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                _imageSource = value;
                OnPropertyChanged();
            }
        }

        public string KeyText
        {
            get
            {
                return _keyText;
            }
            set
            {
                _keyText = value;
                OnPropertyChanged();
            }
        }

        public string ValueText
        {
            get
            {
                return _valueText;
            }
            set
            {
                _valueText = value;
                OnPropertyChanged();
            }
        }

        public bool IsEditable
        {
            get
            {
                return _isEditable;
            }
            set
            {
                _isEditable = value;
                OnPropertyChanged();
            }
        }

        #region INotifyPropertyChanges Handler

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
