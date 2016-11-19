namespace FOMOD.Creator.Domain.Models.ModuleCofiguration
{
    using System;
    using System.Collections.ObjectModel;
    using System.Xml.Serialization;
    using PropertyChanged;

    /// <summary>
    ///     A list of condition flags to set if a <see cref="Plugin" /> is in the
    ///     appropriate state.
    /// </summary>
    [ImplementPropertyChanged]
    [Serializable]
    public class ConditionFlagList
    {
        /// <summary>
        ///     A condition flag to set if the <see cref="Plugin" /> is selected.
        /// </summary>
        [XmlElement("flag")]
        public ObservableCollection<SetConditionFlag> Flag { get; set; }

        public static ConditionFlagList Create()
        {
            return new ConditionFlagList
            {
                Flag = new ObservableCollection<SetConditionFlag>()
            };
        }
    }
}
