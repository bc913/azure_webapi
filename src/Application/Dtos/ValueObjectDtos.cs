using System.Collections.Generic;

namespace Bcan.Backend.Application.Dtos
{
    #region Enums

    public enum DanceTypeDto
    {
        Salsa,
        Bachata,
        ChaCha
    }

    public enum DanceLevelDto
    {
        Beginner,
        Intermediate,
        Advanced,
        All
    }

    public enum IndividualTypeDto
    {
        Undefined,
        Regular,
        Student,
        Veteran,
        All
    }

    public enum PaymentTypeDto
    {
        Undefined,
        NoPayment,
        OneTime,
        Financed
    }

    #endregion

    #region ValueObjects
    public class AddressDto
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }

    public class FeeOptionDto
    {
        public decimal Value { get; set; }
        public IndividualTypeDto Individual { get; set; }
        public PaymentTypeDto Payment { get; set; }
        public string Description { get; set; }
    }

    public class FeeDto
    {
        public IReadOnlyCollection<FeeOptionDto> Options { get; set; }
        public string Description { get; set; }
    }

    public class EventPolicyDto
    {
        public bool DressCode       { get; set; }
        public bool Partner         { get; set; }
        public bool DanceShoes      { get; set; }
        public string Description   { get; set; }
    }

    public class DanceInfoDto
    {
        public DanceLevelDto Level                      { get; set; }
        public IReadOnlyCollection<DanceTypeDto> Types  { get; set; }
    }

    public class MediaResolutionDto
    {
        public string Url { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
    }
    #endregion
}