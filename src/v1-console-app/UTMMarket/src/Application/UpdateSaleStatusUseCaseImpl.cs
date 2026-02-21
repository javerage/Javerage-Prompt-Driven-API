using UTMMarket.Core.Entities;
using UTMMarket.Core.Repositories;
using UTMMarket.Core.UseCases;

namespace UTMMarket.Application;

public sealed class UpdateSaleStatusUseCaseImpl(ISaleRepository repository) : IUpdateSaleStatusUseCase
{
    public async Task<bool> ExecuteAsync(int saleId, SaleStatus newStatus, CancellationToken ct = default)
    {
        var sale = await repository.GetByIdAsync(saleId, ct);
        if (sale is null) return false;

        // Domain Rule Check (Simplified transition rule)
        if (sale.Status == SaleStatus.Canceled && newStatus != SaleStatus.Canceled)
        {
            throw new InvalidOperationException("Cannot change status of a canceled sale.");
        }

        sale.Status = newStatus;
        await repository.UpdateAsync(sale, ct);
        return true;
    }
}
