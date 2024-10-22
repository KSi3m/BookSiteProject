﻿using BookSiteProject.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Application.Commands.BookOfferCommands.CreateBookOffer
{
    public class CreateBookOfferCommand: BookOfferDto, IRequest
    {
        public string BookEncodedName { get; set; } = default!;
    }
}
