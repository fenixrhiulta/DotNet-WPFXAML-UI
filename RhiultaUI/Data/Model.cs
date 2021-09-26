using RhiultaUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;

public class admprodu : ValidatableModel, INotifyPropertyChanged
{
    public int? id { get; set; }
    [NotNull]
    public string codigobarras { get; set; }
    [NotNull]
    public string nome { get; set; }
    public string descricao { get; set; }
    public int? admproduDepartamentoId { get; set; }
    public int? admproduMarcaId { get; set; }
    public int? admproduGrupoId { get; set; }
    public int? admproduTipoId { get; set; }
    public int? admproduUndcomId { get; set; }
    public int? cest { get; set; }
    public int? ncm { get; set; }
    public string obs { get; set; }
    public double? altura { get; set; }
    public double? largura { get; set; }
    public double? comprimento { get; set; }
    public double? peso { get; set; }
    public bool? precad { get; set; }
    public bool? acesso_rapido { get; set; }
    public bool? pesar_caixa { get; set; }
    public bool? etiqueta_balanca { get; set; }
    public bool? is_promo { get; set; }
    public bool? promo_qtd { get; set; }
    public double? promo_preco { get; set; }
    public DateTime? createdAt { get; set; }
    public DateTime? updatedAt { get; set; }
    public int? habilitado { get; set; }
    public decimal? preco { get; set; }

    public admprodu_estoque_preco admprodu_estoque_preco { get; set; }
    public IEnumerable<admprodu_estoque_preco> admprodu_estoque_precos { get; set; }
    public event PropertyChangedEventHandler PropertyChanged;
}


public class admusuario : INotifyPropertyChanged
{
    public int? id { get; set; }
    public DateTime? data { get; set; }
    public string? nome { get; set; }

    public int? admfornecedorId { get; set; }
    public int? admentradaStatusId { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
}

public class admentrada :INotifyPropertyChanged
{
    public int? id { get; set; }
    public DateTime? data { get; set; }
    public int? admnotaId { get; set; }
    public int? admfornecedorId { get; set; }
    public int? admentradaStatusId { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
}

public class admprodu_departamento
{
    public int id { get; set; }
    public string nome { get; set; }
    public DateTime? createdAt { get; set; }
    public DateTime? updatedAt { get; set; }
}

public class admprodu_estoque_preco : ValidatableModel, INotifyPropertyChanged
{
    public int? id { get; set; }
    public int? admproduId { get; set; }
    public int? admfilialId { get; set; }
    public string endereco { get; set; }
    public int? estoque { get; set; }
    public int? qtdMinLoja { get; set; }
    public int? qtdMaxLoja { get; set; }
    public int? expoCapacidade { get; set; }
    public string localizacao { get; set; }
    public decimal? preco { get; set; }
    public int? promo { get; set; }
    public double? promoPreco { get; set; }
    public DateTime? promoDataIni { get; set; }
    public DateTime? promoDataFim { get; set; }
    public DateTime? createdAt { get; set; }
    public DateTime? updatedAt { get; set; }


    //Custons
    public string admfilialNome { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
}



public class admfilial
{
    public int? id { get; set; }
    public string nome { get; set; }
    public string endereco { get; set; }
    public string tipo { get; set; }
    public decimal? markup { get; set; }
    public DateTime? createdAt { get; set; }
    public DateTime? updatedAt { get; set; }
}
