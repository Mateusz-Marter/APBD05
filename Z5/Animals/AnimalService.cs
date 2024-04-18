namespace Z5.Animals
{
    public interface IAnimalService
    {
        public IEnumerable<Animal> GetAllAnimals(string OrderBy);
        public bool AddAnimal(AnimalDTOs dto);
        public bool EditAnimal(AnimalDTOs dto);
        public bool DeleteAnimal(AnimalDTOs dto);
    }

    public class AnimalService : IAnimalService
    {
        private readonly IAnimalRepository _animalRepository;
        public AnimalService(IAnimalRepository animalRepository)
        {
             _animalRepository = animalRepository;
        }

        public IEnumerable<Animal> GetAllAnimals(string orderBy)
        {
            return _animalRepository.FetchAllAnimals(orderBy);
        }

        public bool AddAnimal(AnimalDTOs dto)
        {
            return _animalRepository.CreateAnimal(dto.Name, dto.Category, dto.Area, dto.Description);
        }

        public bool EditAnimal(AnimalDTOs dto)
        {
            return _animalRepository.EditAnimal(dto.Id, dto.Name, dto.Category, dto.Area, dto.Description);
        }

        public bool DeleteAnimal(AnimalDTOs dto)
        {
            return _animalRepository.DeleteAnimal(dto.Id);
        }


    }
}
