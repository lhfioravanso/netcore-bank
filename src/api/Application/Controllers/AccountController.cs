﻿using System;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.Services;
using Domain.Dtos.Request;
using Domain.Dtos.Response;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Application.Controllers
{
    [Authorize("Bearer")]
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService){
            this._accountService = accountService;
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult CreateAccount([FromBody] CreateAccountRequestDto dto)
        {
            try
            {
                CreateAccountResponseDto acc = this._accountService.CreateAccount(dto);
                return Ok(acc);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult GetAccount(int id)
        {
            try
            {
                AccountResponseDto acc = this._accountService.GetAccount(id);
                return Ok(acc);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            
        }

        [HttpGet("{id}/transactions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult GetAccountTransactions(int id)
        {
            try
            {
                IList<AccountTransactionResponseDto> accountTransactions = this._accountService.GetAccountTransactions(id);
                return Ok(accountTransactions);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            
        }

        [HttpPost("{id}/deposit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Deposit(int id, [FromBody] CreateTransactionRequestDto dto)
        {
            try
            {
                CreateTransactionResponseDto response = this._accountService.Deposit(id, dto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            
        }

        [HttpPost("{id}/withdraw")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Withdraw(int id, [FromBody] CreateTransactionRequestDto dto)
        {
            try
            {
                CreateTransactionResponseDto response = this._accountService.Withdraw(id, dto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("{id}/payment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Payment(int id, [FromBody] CreateTransactionRequestDto dto)
        {
            try
            {
                CreateTransactionResponseDto response = this._accountService.Payment(id, dto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
