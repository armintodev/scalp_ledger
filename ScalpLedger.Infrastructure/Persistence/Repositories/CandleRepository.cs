using ScalpLedger.Domain.CandleSticks;
using ScalpLedger.Domain.Repositories;
using ScalpLedger.Infrastructure.Persistence.Repositories.Base;

namespace ScalpLedger.Infrastructure.Persistence.Repositories;

public class CandleRepository(LedgerDbContext context) : EfRepository<Candle>(context), ICandleRepository;