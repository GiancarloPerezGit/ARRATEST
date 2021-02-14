using UnityEngine;
using UnityEngine.UI;

namespace RoboticsAcademy.DataCollection
{
    /// <summary>
    /// Data writer for demographic data.
    /// </summary>
    class DemographicWriter : DataWriter
    {
        [SerializeField] Dropdown age;
        [SerializeField] Dropdown gender;
        [SerializeField] Dropdown race;
        [SerializeField] Dropdown ethnicity;
        [SerializeField] Dropdown maritalStatus;
        [SerializeField] Dropdown numChildren;
        [SerializeField] Dropdown eduLevel;
        [SerializeField] Dropdown employmentStatus;

        /// <summary>
        /// Get demographics capability.
        /// </summary>
        protected override bool InitializeDataProvider() { return true; }

        /// <summary>
        /// Write demographics header.
        /// </summary>
        protected override void WriteHeader()
        {
            // Write header.
            dataStream.WriteLine("Age,Gender,Race,Ethnicity,MaritalStatus,NumChildren,EducationLevel,EmploymentStatus");
        }

        /// <summary>
        /// N/A.
        /// </summary>
        protected override void WriteData() { }

        /// <summary>
        /// Write demographics data.
        /// </summary>
        public void WriteDemographics()
        {
            dataStream.WriteLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7}",
            age.options[age.value].text,
            gender.options[gender.value].text,
            race.options[race.value].text,
            ethnicity.options[ethnicity.value].text,
            maritalStatus.options[maritalStatus.value].text,
            numChildren.options[numChildren.value].text,
            eduLevel.options[eduLevel.value].text,
            employmentStatus.options[employmentStatus.value].text
            ));
        }
    }
}
