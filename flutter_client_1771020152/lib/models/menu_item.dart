class MenuItem {
  final int id;
  final String name;
  final String description;
  final double price;
  final String imageUrl;
  final bool isVegetarian;
  final bool isSpicy;

  MenuItem({
    required this.id,
    required this.name,
    required this.description,
    required this.price,
    required this.imageUrl,
    required this.isVegetarian,
    required this.isSpicy,
  });

  factory MenuItem.fromJson(Map<String, dynamic> json) {
    return MenuItem(
      id: json['Id'] ?? 0, // Chú ý: Web API .NET thường trả về chữ hoa đầu (Id, Name)
      name: json['Name'] ?? 'No Name',
      description: json['Description'] ?? '',
      price: (json['Price'] ?? 0).toDouble(),
      imageUrl: json['ImageUrl'] ?? '',
      isVegetarian: json['IsVegetarian'] ?? false,
      isSpicy: json['IsSpicy'] ?? false,
    );
  }
}