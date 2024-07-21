using AutoMapper;
using BookSiteProject.Application.Dtos;
using BookSiteProject.Domain.Entities;
using BookSiteProject.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }
        public async Task Create(AuthorDto authorDto)
        {
            var author = _mapper.Map<Author>(authorDto);
            await _authorRepository.Create(author);
        }

        public async Task<IEnumerable<Author>> GetAuthors()
        {
            return await _authorRepository.GetAll();
        }
        public async Task<IEnumerable<AuthorDto>> GetAllAuthorsDto()
        {
            var authors = await this.GetAuthors();
            var dtos = _mapper.Map<IEnumerable<AuthorDto>>(authors);
            return dtos;
        }
    }
}

