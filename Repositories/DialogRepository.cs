﻿using OriginsOfDestiny.Data;
using OriginsOfDestiny.Models.Dialogs;
using System.Linq.Expressions;

namespace OriginsOfDestiny.Repositories
{
    public class DialogRepository : IRepository<Dialog>
    {
        private readonly ApplicationDbContext _dbContext;

        public DialogRepository(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public IQueryable<Dialog> Get(Expression<Func<Dialog, bool>> expression)
        {
            if (!_dbContext.Dialogs.Any())
            {
                _dbContext.Dialogs.Add(new Dialog()
                {
                    Id = @"\start",
                    Text = "Доброе утро, Соня!",
                    Responses = {
                        {"start_wmi", "Где я?" }
                    }
                });

                _dbContext.Dialogs.Add(new Dialog()
                {
                    Id = "start_wmi",
                    Text = "Догадайся",
                    Responses = {
                        {"start_no", "Неа" },
                        {"start__", "*Промолчать*" }
                    }
                });

                _dbContext.SaveChanges();
            }

            return _dbContext.Dialogs.Where(expression);
        }
    }
}
