using Prism;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using FiefApp.Common.Infrastructure.Services;

namespace FiefApp.Common.Infrastructure
{
    public abstract class ViewModelBaseClass : BindableBase, IActiveAware
    {
        private readonly IBaseService _baseService;

        protected ViewModelBaseClass(IBaseService baseService)
        {
            _baseService = baseService;
        }

        protected bool RemovedFief;

        private int _index;
        public int Index
        {
            get => _index;
            set
            {
                int oldIndex = _index;
                if (SetProperty(ref _index, value))
                {
                    if (!Activating)
                    {
                        OnIndexChanged(oldIndex, value);
                    }
                }
            }
        }

        public string TabName { get; set; }

        #region Fief ComboBox

        private ObservableCollection<string> _fiefCollection =
            new ObservableCollection<string>()
            {
                "Alla",
                ""
            };
        public ObservableCollection<string> FiefCollection
        {
            get => _fiefCollection;
            set => SetProperty(ref _fiefCollection, value);
        }

        #endregion

        #region IActiveAware

        private bool _isActive { get; set; }
        private bool _activating;
        protected bool Activating
        {
            get => _activating;
            set => SetProperty(ref _activating, value);
        }

        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                if (value)
                {
                    Activating = true;
                    OnActivate();
                    Activating = false;
                }
                else
                {
                    OnDeactivate();
                }
                IsActiveChanged(this, EventArgs.Empty);
            }
        }

        protected virtual void OnActivate()
        {
            Index = _baseService.GetIndex();
            LoadData();
        }

        protected virtual void OnDeactivate()
        {
            _baseService.SetIndex(Index);
            SaveData();
        }

        protected virtual void OnIndexChanged(int oldIndex, int newIndex)
        {
            if (oldIndex < 1)
            {
                LoadData();
            }
            else
            {
                if (RemovedFief)
                {
                    RemovedFief = false;
                }
                else
                {
                    if (oldIndex != -1)
                    {
                        SaveData(oldIndex);
                        LoadData();
                    }
                    else
                    {
                        LoadData();
                    }
                }
            }
        }

        protected virtual void UpdateFiefCollection()
        {
            Activating = true;
            int tempIndex = Index;
            FiefCollection = _baseService.GetFiefCollection();
            Index = tempIndex;
            Activating = false;
        }
        protected abstract void SaveData(int index = -1);
        protected abstract void LoadData();

        public event EventHandler IsActiveChanged = delegate { };

        #endregion
    }
}
