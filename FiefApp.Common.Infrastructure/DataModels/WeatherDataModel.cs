using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FiefApp.Common.Infrastructure.DataModels
{
    public class WeatherDataModel : INotifyPropertyChanged, ICloneable, IDataModelBase
    {
        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                NotifyPropertyChanged();
            }
        }

        private int _springRoll;
        public int SpringRoll
        {
            get => _springRoll;
            set
            {
                _springRoll = value;
                SpringRollMod = CheckWeatherMod(value);
                CalculateTotalWeatherHappiness();
                NotifyPropertyChanged();
            }
        }

        private int _springRollMod;
        public int SpringRollMod
        {
            get => _springRollMod;
            set
            {
                _springRollMod = value;
                SpringShow = ConvertToT6Value(value);
                NotifyPropertyChanged();
            }
        }

        private string _springShow;
        public string SpringShow
        {
            get => _springShow;
            set
            {
                _springShow = value;
                NotifyPropertyChanged();
            }
        }

        private int _summerRoll;
        public int SummerRoll
        {
            get => _summerRoll;
            set
            {
                _summerRoll = value;
                SummerRollMod = CheckWeatherMod(value);
                CalculateTotalWeatherHappiness();
                NotifyPropertyChanged();
            }
        }

        private int _summerRollMod;
        public int SummerRollMod
        {
            get => _summerRollMod;
            set
            {
                _summerRollMod = value;
                SummerShow = ConvertToT6Value(value);
                NotifyPropertyChanged();
            }
        }

        private string _summerShow;
        public string SummerShow
        {
            get => _summerShow;
            set
            {
                _summerShow = value;
                NotifyPropertyChanged();
            }
        }

        private int _fallRoll;
        public int FallRoll
        {
            get => _fallRoll;
            set
            {
                _fallRoll = value;
                FallRollMod = CheckWeatherMod(value);
                CalculateTotalWeatherHappiness();
                NotifyPropertyChanged();
            }
        }

        private int _fallRollMod;
        public int FallRollMod
        {
            get => _fallRollMod;
            set
            {
                _fallRollMod = value;
                FallShow = ConvertToT6Value(value);
                NotifyPropertyChanged();
            }
        }

        private string _fallShow;
        public string FallShow
        {
            get => _fallShow;
            set
            {
                _fallShow = value;
                NotifyPropertyChanged();
            }
        }

        private int _winterRoll;
        public int WinterRoll
        {
            get => _winterRoll;
            set
            {
                _winterRoll = value;
                WinterRollMod = CheckWeatherMod(value);
                CalculateTotalWeatherHappiness();
                NotifyPropertyChanged();
            }
        }

        private int _winterRollMod;
        public int WinterRollMod
        {
            get => _winterRollMod;
            set
            {
                _winterRollMod = value;
                WinterShow = ConvertToT6Value(value);
                NotifyPropertyChanged();
            }
        }

        private string _winterShow;
        public string WinterShow
        {
            get => _winterShow;
            set
            {
                _winterShow = value;
                NotifyPropertyChanged();
            }
        }

        private string _otherMod;
        public string OtherMod
        {
            get => _otherMod;
            set
            {
                _otherMod = value;
                CalculateTotalWeatherHappiness();
                NotifyPropertyChanged();
            }
        }

        private string _happinessCrime = "0";
        public string HappinessCrime
        {
            get => _happinessCrime;
            set
            {
                _happinessCrime = value;
                SetHappinessTotal();
                NotifyPropertyChanged();
            }
        }

        private string _happinessBuildings = "0";
        public string HappinessBuildings
        {
            get => _happinessBuildings;
            set
            {
                _happinessBuildings = value;
                SetHappinessTotal();
                NotifyPropertyChanged();
            }
        }

        private string _happinessDaysWorkRequired = "0";
        public string HappinessDaysWorkRequired
        {
            get => _happinessDaysWorkRequired;
            set
            {
                _happinessDaysWorkRequired = value;
                SetHappinessTotal();
                NotifyPropertyChanged();
            }
        }

        private string _happinessWar = "0";
        public string HappinessWar
        {
            get => _happinessWar;
            set
            {
                _happinessWar = value;
                SetHappinessTotal();
                NotifyPropertyChanged();
            }
        }

        private string _happinessTaxes = "0";
        public string HappinessTaxes
        {
            get => _happinessTaxes;
            set
            {
                _happinessTaxes = value;
                SetHappinessTotal();
                NotifyPropertyChanged();
            }
        }

        private string _happinessDevelopmentLevels = "0";
        public string HappinessDevelopmentLevels
        {
            get => _happinessDevelopmentLevels;
            set
            {
                _happinessDevelopmentLevels = value;
                SetHappinessTotal();
                NotifyPropertyChanged();
            }
        }

        private string _happinessWeather = "0";
        public string HappinessWeather
        {
            get => _happinessWeather;
            set
            {
                _happinessWeather = value;
                SetHappinessTotal();
                NotifyPropertyChanged();
            }
        }

        private string _happinessTotal = "0";
        public string HappinessTotal
        {
            get => _happinessTotal;
            set
            {
                _happinessTotal = value;
                NotifyPropertyChanged();
            }
        }

        private int _dayworkers;
        public int Dayworkers
        {
            get => _dayworkers;
            set
            {
                _dayworkers = value;
                DayworkersDayswork = value * 280;
                NotifyPropertyChanged();
            }
        }

        private int _dayworkersDayswork;
        public int DayworkersDayswork
        {
            get => _dayworkersDayswork;
            set
            {
                _dayworkersDayswork = value;
                SetDaysworkTotal();
                CalculateDaysworkLeft();
                NotifyPropertyChanged();
            }
        }

        private int _serfs;
        public int Serfs
        {
            get => _serfs;
            set
            {
                _serfs = value;
                SerfsDayswork = value * DaysworkRequired;
                NotifyPropertyChanged();
            }
        }

        private int _serfsDayswork;
        public int SerfsDayswork
        {
            get => _serfsDayswork;
            set
            {
                _serfsDayswork = value;
                SetDaysworkTotal();
                CalculateDaysworkLeft();
                NotifyPropertyChanged();
            }
        }

        private int _daysworkRequired = 80;
        public int DaysworkRequired
        {
            get => _daysworkRequired;
            set
            {
                _daysworkRequired = value;
                SerfsDayswork = value * Serfs;
                UpdateHappiness();
                NotifyPropertyChanged();
            }
        }

        private int _slaves;
        public int Slaves
        {
            get => _slaves;
            set
            {
                _slaves = value;
                SlavesDayswork = value * 300;
                NotifyPropertyChanged();
            }
        }

        private int _slavesDayswork;
        public int SlavesDayswork
        {
            get => _slavesDayswork;
            set
            {
                _slavesDayswork = value;
                SetDaysworkTotal();
                CalculateDaysworkLeft();
                NotifyPropertyChanged();
            }
        }

        private int _daysworkOther;
        public int DaysworkOther
        {
            get => _daysworkOther;
            set
            {
                _daysworkOther = value;
                SetDaysworkTotal();
                CalculateDaysworkLeft();
                NotifyPropertyChanged();
            }
        }

        private int _daysworkTotal;
        public int DaysworkTotal
        {
            get => _daysworkTotal;
            set
            {
                _daysworkTotal = value;
                NotifyPropertyChanged();
            }
        }

        private int _manorDaysWork;
        public int ManorDaysWork
        {
            get => _manorDaysWork;
            set
            {
                _manorDaysWork = value;
                CalculateDaysworkLeft();
                NotifyPropertyChanged();
            }
        }

        private int _numberOfSubsidiaries;
        public int NumberOfSubsidiaries
        {
            get => _numberOfSubsidiaries;
            set
            {
                _numberOfSubsidiaries = value;
                NotifyPropertyChanged();
            }
        }

        private int _subsidiariesDayswork;
        public int SubsidiariesDayswork
        {
            get => _subsidiariesDayswork;
            set
            {
                _subsidiariesDayswork = value;
                CalculateDaysworkLeft();
                NotifyPropertyChanged();
            }
        }

        private int _numberOfFishingBoats;
        public int NumberOfFishingBoats
        {
            get => _numberOfFishingBoats;
            set
            {
                _numberOfFishingBoats = value;
                NotifyPropertyChanged();
            }
        }

        private int _numberUsedFishingBoats;
        public int NumberUsedFishingBoats
        {
            get => _numberUsedFishingBoats;
            set
            {
                if (value < 0)
                {
                    _numberUsedFishingBoats = 0;
                    DaysworkFishingBoats = 0;
                }
                else
                {
                    if (value > NumberOfFishingBoats)
                    {
                        _numberUsedFishingBoats = NumberOfFishingBoats;
                        DaysworkFishingBoats = NumberOfFishingBoats * 360;
                    }
                    else
                    {
                        _numberUsedFishingBoats = value;
                        DaysworkFishingBoats = value * 360;
                    }
                }
                NotifyPropertyChanged();
            }
        }

        private int _daysworkFishingBoats;
        public int DaysworkFishingBoats
        {
            get => _daysworkFishingBoats;
            set
            {
                _daysworkFishingBoats = value;
                CalculateDaysworkLeft();
                NotifyPropertyChanged();
            }
        }

        private int _daysworkFishingBoatsRemaining;
        public int DaysworkFishingBoatsRemaining
        {
            get => _daysworkFishingBoatsRemaining;
            set
            {
                _daysworkFishingBoatsRemaining = value;
                NotifyPropertyChanged();
            }
        }

        private int _landClearing;
        public int LandClearing
        {
            get => _landClearing;
            set
            {
                if (value < 0)
                {
                    _landClearing = 0;
                    DaysworkLandClearing = 0;
                }
                else if (value > LandClearingMax)
                {
                    _landClearing = LandClearingMax;
                    DaysworkLandClearing = LandClearingMax * 280;
                }
                else
                {
                    _landClearing = value;
                    DaysworkLandClearing = value * 280;
                }
                NotifyPropertyChanged();
            }
        }

        private int _landClearingMax;
        public int LandClearingMax
        {
            get => _landClearingMax;
            set
            {
                _landClearingMax = value;
                NotifyPropertyChanged();
            }
        }

        private int _daysworkLandClearing;
        public int DaysworkLandClearing
        {
            get => _daysworkLandClearing;
            set
            {
                _daysworkLandClearing = value;
                CalculateDaysworkLeft();
                NotifyPropertyChanged();
            }
        }

        private int _daysworkLandClearingRemaining;
        public int DaysworkLandClearingRemaining
        {
            get => _daysworkLandClearingRemaining;
            set
            {
                _daysworkLandClearingRemaining = value;
                NotifyPropertyChanged();
            }
        }

        private int _landClearingOfFelling;
        public int LandClearingOfFelling
        {
            get => _landClearingOfFelling;
            set
            {
                if (value < 0)
                {
                    _landClearingOfFelling = 0;
                    DaysworkLandClearingOfFelling = 0;
                }
                else if (value > LandClearingOfFellingMax)
                {
                    _landClearingOfFelling = LandClearingOfFellingMax;
                    DaysworkLandClearingOfFelling = 210 * LandClearingOfFellingMax;
                }
                else
                {
                    _landClearingOfFelling = value;
                    DaysworkLandClearingOfFelling = value * 210;
                }
                NotifyPropertyChanged();
            }
        }

        private int _landClearingOfFellingMax;
        public int LandClearingOfFellingMax
        {
            get => _landClearingOfFellingMax;
            set
            {
                _landClearingOfFellingMax = value;
                NotifyPropertyChanged();
            }
        }

        private int _daysworkLandClearingOfFelling;
        public int DaysworkLandClearingOfFelling
        {
            get => _daysworkLandClearingOfFelling;
            set
            {
                _daysworkLandClearingOfFelling = value;
                CalculateDaysworkLeft();
                NotifyPropertyChanged();
            }
        }

        private int _daysworkLandClearingOfFellingRemaining;
        public int DaysworkLandClearingOfFellingRemaining
        {
            get => _daysworkLandClearingOfFellingRemaining;
            set
            {
                _daysworkLandClearingOfFellingRemaining = value;
                NotifyPropertyChanged();
            }
        }

        private int _clearUseless;
        public int ClearUseless
        {
            get => _clearUseless;
            set
            {
                if (value < 0)
                {
                    _clearUseless = 0;
                    DaysworkClearUseless = 0;
                }
                else if (value > ClearUselessMax)
                {
                    _clearUseless = ClearUselessMax;
                    DaysworkClearUseless = 500 * ClearUselessMax;
                }
                else
                {
                    _clearUseless = value;
                    DaysworkClearUseless = value * 500;
                }
                NotifyPropertyChanged();
            }
        }

        private int _clearUselessMax;
        public int ClearUselessMax
        {
            get => _clearUselessMax;
            set
            {
                _clearUselessMax = value;
                NotifyPropertyChanged();
            }
        }

        private int _daysworkClearUseless;
        public int DaysworkClearUseless
        {
            get => _daysworkClearUseless;
            set
            {
                _daysworkClearUseless = value;
                CalculateDaysworkLeft();
                NotifyPropertyChanged();
            }
        }

        private int _clearUselessRemaining;
        public int ClearUselessRemaining
        {
            get => _clearUselessRemaining;
            set
            {
                _clearUselessRemaining = value;
                NotifyPropertyChanged();
            }
        }

        private int _felling;
        public int Felling
        {
            get => _felling;
            set
            {
                if (value < 0)
                {
                    _felling = 0;
                    DaysworkFelling = 0;
                }
                else if (value > FellingMax)
                {
                    _felling = FellingMax;
                    DaysworkFelling = 70 * FellingMax;
                }
                else
                {
                    _felling = value;
                    DaysworkFelling = value * 70;
                }
                NotifyPropertyChanged();
            }
        }

        private int _fellingMax;
        public int FellingMax
        {
            get => _fellingMax;
            set
            {
                _fellingMax = value;
                NotifyPropertyChanged();
            }
        }

        private int _daysworkFelling;
        public int DaysworkFelling
        {
            get => _daysworkFelling;
            set
            {
                _daysworkFelling = value;
                CalculateDaysworkLeft();
                NotifyPropertyChanged();
            }
        }

        private int _fellingRemaining;
        public int FellingRemaining
        {
            get => _fellingRemaining;
            set
            {
                _fellingRemaining = value;
                NotifyPropertyChanged();
            }
        }

        private int _manorUpkeep;
        public int ManorUpkeep
        {
            get => _manorUpkeep;
            set
            {
                _manorUpkeep = value;
                CalculateDaysworkLeft();
                NotifyPropertyChanged();
            }
        }

        private int _daysworkOtherExpend;
        public int DaysworkOtherExpend
        {
            get => _daysworkOtherExpend;
            set
            {
                _daysworkOtherExpend = value;
                CalculateDaysworkLeft();
                NotifyPropertyChanged();
            }
        }

        private int _daysworkLeft;
        public int DaysworkLeft
        {
            get => _daysworkLeft;
            set
            {
                _daysworkLeft = value;
                CalculateRemaining();
                NotifyPropertyChanged();
            }
        }

        private int _thisYearSilver;
        public int ThisYearSilver
        {
            get => _thisYearSilver;
            set
            {
                _thisYearSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _thisYearBase;
        public int ThisYearBase
        {
            get => _thisYearBase;
            set
            {
                _thisYearBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _thisYearLuxury;
        public int ThisYearLuxury
        {
            get => _thisYearLuxury;
            set
            {
                _thisYearLuxury = value;
                NotifyPropertyChanged();
            }
        }

        private int _thisYearIron;
        public int ThisYearIron
        {
            get => _thisYearIron;
            set
            {
                _thisYearIron = value;
                NotifyPropertyChanged();
            }
        }

        private int _thisYearStone;
        public int ThisYearStone
        {
            get => _thisYearStone;
            set
            {
                _thisYearStone = value;
                NotifyPropertyChanged();
            }
        }

        private int _thisYearWood;
        public int ThisYearWood
        {
            get => _thisYearWood;
            set
            {
                _thisYearWood = value;
                NotifyPropertyChanged();
            }
        }

        private int _lastYearSilver;
        public int LastYearSilver
        {
            get => _lastYearSilver;
            set
            {
                _lastYearSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _lastYearBase;
        public int LastYearBase
        {
            get => _lastYearBase;
            set
            {
                _lastYearBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _lastYearLuxury;
        public int LastYearLuxury
        {
            get => _lastYearLuxury;
            set
            {
                _lastYearLuxury = value;
                NotifyPropertyChanged();
            }
        }

        private int _lastYearIron;
        public int LastYearIron
        {
            get => _lastYearIron;
            set
            {
                _lastYearIron = value;
                NotifyPropertyChanged();
            }
        }

        private int _lastYearStone;
        public int LastYearStone
        {
            get => _lastYearStone;
            set
            {
                _lastYearStone = value;
                NotifyPropertyChanged();
            }
        }

        private int _lastYearWood;
        public int LastYearWood
        {
            get => _lastYearWood;
            set
            {
                _lastYearWood = value;
                NotifyPropertyChanged();
            }
        }

        private int _tariffs = 10;
        public int Tariffs
        {
            get => _tariffs;
            set
            {
                _tariffs = value;
                NotifyPropertyChanged();
            }
        }

        private int _taxSerfs = 20;
        public int TaxSerfs
        {
            get => _taxSerfs;
            set
            {
                _taxSerfs = value;
                NotifyPropertyChanged();
            }
        }

        private int _taxFarmers = 20;
        public int TaxFarmers
        {
            get => _taxFarmers;
            set
            {
                _taxFarmers = value;
                NotifyPropertyChanged();
            }
        }

        private int _taxFreemen = 20;
        public int TaxFreemen
        {
            get => _taxFreemen;
            set
            {
                _taxFreemen = value;
                NotifyPropertyChanged();
            }
        }

        private int _taxVasalls = 10;
        public int TaxVasalls
        {
            get => _taxVasalls;
            set
            {
                _taxVasalls = value;
                NotifyPropertyChanged();
            }
        }

        private int _taxToLiegeLord = 10;
        public int TaxToLiegeLord
        {
            get => _taxToLiegeLord;
            set
            {
                _taxToLiegeLord = value;
                NotifyPropertyChanged();
            }
        }

        private int _taxToTheKing = 10;
        public int TaxToTheKing
        {
            get => _taxToTheKing;
            set
            {
                _taxToTheKing = value;
                NotifyPropertyChanged();
            }
        }

        private string _endOfYearError;
        public string EndOfYearError
        {
            get => _endOfYearError;
            set
            {
                _endOfYearError = value;
                NotifyPropertyChanged();
            }
        }

        #region Weather Methods

        private int CheckWeatherMod(int i)
        {
            if (i == 3)
            {
                return 8;
            }
            else if (i <= 5)
            {
                return 4;
            }
            else if (i <= 7)
            {
                return 2;
            }
            else if (i <= 9)
            {
                return 1;
            }
            else if (i <= 11)
            {
                return 0;
            }
            else if (i <= 13)
            {
                return -1;
            }
            else if (i <= 15)
            {
                return -2;
            }
            else if (i <= 17)
            {
                return -4;
            }
            else
            {
                return -Convert.ToInt32(Math.Floor((decimal)i / 3));
            }
        }

        private void CalculateTotalWeatherHappiness()
        {
            int tempi = SpringRollMod + SummerRollMod + FallRollMod + WinterRollMod;

            int mod = 0;
            if (OtherMod != null)
            {
                if (OtherMod == "")
                {
                    mod += 0;
                }
                else
                {
                    if (OtherMod.IndexOf('T') != -1 || OtherMod.Length < 3)
                    {
                        bool isNegative;
                        string temp;

                        if (OtherMod.IndexOf('-') == -1)
                        {
                            temp = OtherMod;
                            isNegative = false;
                        }
                        else
                        {
                            temp = OtherMod;
                            temp = temp.Substring(1);
                            isNegative = true;
                        }
                        if (temp.IndexOf('+') == 0)
                        {
                            temp = temp.Substring(1);
                        }


                        string temp2;
                        var value = 0;
                        var loop = true;
                        var holder = "";

                        if (temp.Length < 3)
                        {
                            value = Convert.ToInt32(temp);
                        }
                        else
                        {
                            int x;
                            for (x = 0; x < temp.Length && loop; x++)
                            {
                                if (Char.IsDigit(temp[x]))
                                {
                                    temp2 = temp;
                                    holder = holder + temp2[x];
                                }
                                else
                                {
                                    value = Convert.ToInt32(holder);
                                    value = value * 4;
                                    loop = false;
                                }
                            }
                        }
                        if (temp.IndexOf('+') != -1)
                        {
                            var pos = temp.IndexOf('+');
                            pos = pos + 1;
                            var y = temp.Substring(pos, 1);
                            value = value + Convert.ToInt32(y);
                        }
                        if (isNegative)
                        {
                            mod -= value;
                        }
                        else
                        {
                            mod += value;
                        }
                    }
                }
            }

            HappinessWeather = ConvertToT6Value(tempi + mod);
        }

        #endregion

        #region Dayswork Methods

        private void SetDaysworkTotal()
        {
            DaysworkTotal =
                DayworkersDayswork
                + SerfsDayswork
                + SlavesDayswork
                + DaysworkOther;
        }

        private void CalculateDaysworkLeft()
        {
            DaysworkLeft = DaysworkTotal
                           - ManorDaysWork
                           - SubsidiariesDayswork
                           - DaysworkFishingBoats
                           - DaysworkLandClearing
                           - DaysworkLandClearingOfFelling
                           - DaysworkClearUseless
                           - DaysworkFelling
                           - ManorUpkeep
                           - DaysworkOtherExpend;
        }

        private void CalculateRemaining()
        {
            if (DaysworkLeft > 0)
            {
                DaysworkFishingBoatsRemaining = Convert.ToInt32(Math.Floor((decimal)DaysworkLeft / 360));
                DaysworkLandClearingRemaining = Convert.ToInt32(Math.Floor((decimal)DaysworkLeft / 280));
                DaysworkLandClearingOfFellingRemaining = Convert.ToInt32(Math.Floor((decimal)DaysworkLeft / 210));
                ClearUselessRemaining = Convert.ToInt32(Math.Floor((decimal)DaysworkLeft / 500));
                FellingRemaining = Convert.ToInt32(Math.Floor((decimal)DaysworkLeft / 70));
            }
            else
            {
                DaysworkFishingBoatsRemaining = 0;
                DaysworkLandClearingRemaining = 0;
                DaysworkLandClearingOfFellingRemaining = 0;
                ClearUselessRemaining = 0;
                FellingRemaining = 0;
            }

        }

        #endregion

        #region Happiness Methods

        private void UpdateHappiness()
        {
            int happinessNr;

            if (DaysworkRequired - 80 > 0)
            {
                if (DaysworkRequired > 120)
                {
                    happinessNr = Convert.ToInt32(Math.Ceiling(((decimal)DaysworkRequired - 80) / 3.66M));
                }
                else
                {
                    happinessNr = Convert.ToInt32(Math.Ceiling(((decimal)DaysworkRequired - 80) / 5.37M));
                }

            }
            else
            {
                happinessNr = Convert.ToInt32(Math.Floor(((decimal)DaysworkRequired - 80) / 6.66M));
            }

            HappinessDaysWorkRequired = ConvertToT6Value(happinessNr);
        }

        private void SetHappinessTotal()
        {
            HappinessTotal = ConvertToT6Value(
                ConvertToT6Value(HappinessDaysWorkRequired)
                + ConvertToT6Value(HappinessCrime)
                + ConvertToT6Value(HappinessBuildings)
                + ConvertToT6Value(HappinessDevelopmentLevels)
                + ConvertToT6Value(HappinessTaxes)
                + ConvertToT6Value(HappinessWar)
                + ConvertToT6Value(HappinessWeather)
            );
        }

        #endregion

        #region Misc Methods

        private string ConvertToT6Value(int v)
        {
            string temp = Convert.ToString(v);
            string value;
            bool isNegative;

            if (temp.IndexOf('-') != -1)
            {
                temp = temp.Substring(1);
                isNegative = true;
            }
            else
            {
                isNegative = false;
            }


            var y = Convert.ToInt32(temp);
            var i = y / 4;
            var x = y - i * 4;

            if (!isNegative)
            {
                if (x > 0)
                {
                    if (i != 0)
                    {
                        value = i + "T6+" + x;
                    }
                    else
                    {
                        value = "+" + x;
                    }
                }
                else
                {
                    if (i != 0)
                    {
                        value = i + "T6";
                    }
                    else
                    {
                        value = "0";
                    }
                }
            }
            else
            {
                if (x > 0)
                {
                    if (i != 0)
                    {
                        value = "-" + i + "T6+" + x;
                    }
                    else
                    {
                        value = "-" + x;
                    }
                }
                else
                {
                    if (i != 0)
                    {
                        value = "-" + i + "T6";
                    }
                    else
                    {
                        value = "0";
                    }
                }
            }
            return value;
        }

        private int ConvertToT6Value(string str)
        {
            bool isNegative;
            string temp;

            if (str.IndexOf('-') == -1)
            {
                temp = str;
                isNegative = false;
            }
            else
            {
                temp = str;
                temp = temp.Substring(1);
                isNegative = true;
            }
            if (temp.IndexOf('+') == 0)
            {
                temp = temp.Substring(1);
            }


            string temp2;
            var value = 0;
            var loop = true;
            var holder = "";

            if (temp.Length < 3)
            {
                value = Convert.ToInt32(temp);
            }
            else
            {
                int x;
                for (x = 0; x < temp.Length && loop; x++)
                {
                    if (Char.IsDigit(temp[x]))
                    {
                        temp2 = temp;
                        holder = holder + temp2[x];
                    }
                    else
                    {
                        value = Convert.ToInt32(holder);
                        value = value * 4;
                        loop = false;
                    }
                }
            }
            if (temp.IndexOf('+') != -1)
            {
                var pos = temp.IndexOf('+');
                pos = pos + 1;
                var y = temp.Substring(pos, 1);
                value = value + Convert.ToInt32(y);
            }
            if (isNegative)
            {
                return -value;
            }
            else
            {
                return value;
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region ICloneable

        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion
    }
}
