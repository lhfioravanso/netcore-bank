using Microsoft.EntityFrameworkCore;

namespace Infra.Context {

    public class ApiDbContext: DbContext {

        public ApiDbContext(DbContextOptions<ApiDbContext> options): base(options) {

        }
    }
}