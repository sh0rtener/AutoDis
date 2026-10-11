using AutoDis.Domain.Cars;
using AutoDis.Persistense.EntityFramework.Entities;
using AutoMapper;

namespace AutoDis.Persistense.EntityFramework;

public class CarCountryProfile : Profile
{
    public CarCountryProfile()
    {
        CreateMap<CarCountryEntity, CarCountry>()
            .ConstructUsing((src, ctx) => new CarCountry(src.Name, src.IsoCode));

        CreateMap<CarCountry, CarCountryEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Manufacturers, opt => opt.Ignore());
    }
}

public class CarManufacturerProfile : Profile
{
    public CarManufacturerProfile()
    {
        CreateMap<CarManufacturerEntity, CarManufacturer>()
            .ConstructUsing(
                (src, ctx) => new CarManufacturer(src.Name, ctx.Mapper.Map<CarCountry>(src.Country))
            );

        CreateMap<CarManufacturer, CarManufacturerEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Cars, opt => opt.Ignore())
            .ForMember(dest => dest.Country, opt => opt.Ignore())
            .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.Country.Id));
    }
}

public class CarProfile : Profile
{
    public CarProfile()
    {
        CreateMap<CarEntity, Car>()
            .ConstructUsing(
                (src, ctx) =>
                    new Car(
                        src.Model,
                        ctx.Mapper.Map<CarManufacturer>(src.Manufacturer),
                        src.Year,
                        (CarColor)src.ColorType,
                        (CarBodyType)src.BodyType,
                        (CarFuelType)src.FuelType,
                        (CarTransmissionType)src.TransmissionType,
                        (CarDriveTrain)src.DriveTrain,
                        new Power(src.Power),
                        src.Doors,
                        src.Seats,
                        new Mileage(src.Mileage)
                    )
            );

        CreateMap<Car, CarEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Manufacturer, opt => opt.Ignore())
            .ForMember(dest => dest.ManufacturerId, opt => opt.MapFrom(src => src.Manufacturer.Id))
            .ForMember(dest => dest.ColorType, opt => opt.MapFrom(src => (int)src.Color))
            .ForMember(dest => dest.BodyType, opt => opt.MapFrom(src => (int)src.BodyType))
            .ForMember(dest => dest.FuelType, opt => opt.MapFrom(src => (int)src.FuelType))
            .ForMember(
                dest => dest.TransmissionType,
                opt => opt.MapFrom(src => (int)src.TransmissionType)
            )
            .ForMember(dest => dest.DriveTrain, opt => opt.MapFrom(src => (int)src.DriveTrain))
            .ForMember(dest => dest.Power, opt => opt.MapFrom(src => src.Power.Horsepower))
            .ForMember(dest => dest.Mileage, opt => opt.MapFrom(src => src.Mileage.Km));
    }
}

public class CarBillNoteProfile : Profile
{
    public CarBillNoteProfile()
    {
        CreateMap<CarBillNoteEntity, CarBillNote>()
            .ConstructUsing((src, ctx) => new CarBillNote(src.Name, src.Cost, src.Available));

        CreateMap<CarBillNote, CarBillNoteEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CarBill, opt => opt.Ignore())
            .ForMember(dest => dest.CarBillId, opt => opt.Ignore());
    }
}

public class CarBillImageProfile : Profile
{
    public CarBillImageProfile()
    {
        CreateMap<CarBillImageEntity, string>().ConstructUsing(src => src.ImagePath);

        CreateMap<string, CarBillImageEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CarBill, opt => opt.Ignore())
            .ForMember(dest => dest.CarBillId, opt => opt.Ignore())
            .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src));
    }
}

public class CarBillProfile : Profile
{
    public CarBillProfile()
    {
        CreateMap<CarBillEntity, CarBill>()
            .ConstructUsing(
                (src, ctx) =>
                {
                    var car = ctx.Mapper.Map<Car>(src.Car);
                    var bill = new CarBill(car);

                    foreach (var note in src.Notes)
                    {
                        bill.AddNote(ctx.Mapper.Map<CarBillNote>(note));
                    }

                    foreach (var image in src.Images)
                    {
                        bill.AddImage(image.ImagePath);
                    }

                    if (src.IsArchieve)
                    {
                        bill.Archive();
                    }

                    bill.ChangeCost(src.Cost);

                    return bill;
                }
            );

        CreateMap<CarBill, CarBillEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Car, opt => opt.Ignore())
            .ForMember(dest => dest.CarId, opt => opt.Ignore())
            .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.ImagePaths));
    }
}
