namespace SwAppData.Entity;

/// <summary>
///     Bir adet olmalı homeda 3 adet var ise burada birden falza kayıt eklenip listelenip çekilir gösterilir
///     summary yazmak isterseniz metodun üstüne gelip 3 kere /// isareti yaparsanız otomatik açacaktir.
/// </summary>
public class HomeWhySorsware : BaseEntity
{
    public string HomeWhySorswareSubTitle { get; set; }
    public string HomeWhySorswareSubDetails { get; set; }
    public string? HomeWhySorswareSubImageUrl { get; set; }
}