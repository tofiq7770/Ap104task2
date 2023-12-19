using Ap104.Dtos.Tag;
using Ap104.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ap104.Services.Implementations
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _repository;
        public TagService(ITagRepository repository)
        {
            _repository = repository;
        }
        public async Task<ICollection<GetTagDto>> GetAllAsync(int page, int take)
        {
            ICollection<Tag> tags = await _repository.GetAllAsync(skip: (page - 1) * take, take: take, isTracking: false).ToListAsync();

            ICollection<GetTagDto> tagDtos = new List<GetTagDto>();
            foreach (var tag in tags)
            {
                tagDtos.Add(new GetTagDto
                {
                    Id = tag.Id,
                    Name = tag.Name
                });
            }
            return tagDtos;
        }

        public async Task<GetTagDto> GetAsync(int id)
        {
            Tag tag = await _repository.GetByIdAsync(id);
            if (tag == null) throw new Exception("Not Found");

            return new GetTagDto
            {
                Id = tag.Id,
                Name = tag.Name
            };
        }

        public async Task CreateAsync(CreateTagDto tagDto)
        {
            await _repository.AddAsync(new Tag
            {
                Name = tagDto.Name,
            });
            await _repository.SaveChangesAsync();
        }
        public async Task UpdateAsync(int id, UpdateTagDto updateTagDto)
        {
            Tag tag = await _repository.GetByIdAsync(id);
            if (tag is null) throw new Exception("Not Found");
            tag.Name = updateTagDto.Name;
            _repository.Update(tag);
            await _repository.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            Tag tag = await _repository.GetByIdAsync(id);

            if (tag is null) throw new Exception("Not Found");

            _repository.Delete(tag);
            await _repository.SaveChangesAsync();
        }


    }
}
