import 'dart:io';
import 'package:flutter/material.dart';
import 'models/menu_item.dart';
import 'services/api_service.dart';

// --- PHẦN 1: BỎ QUA LỖI SSL (Chỉ dùng cho môi trường Dev) ---
class MyHttpOverrides extends HttpOverrides {
  @override
  HttpClient createHttpClient(SecurityContext? context) {
    return super.createHttpClient(context)
      ..badCertificateCallback = (X509Certificate cert, String host, int port) => true;
  }
}

void main() {
  // Kích hoạt thuốc trị lỗi SSL
  HttpOverrides.global = MyHttpOverrides();
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      home: const MenuScreen(),
    );
  }
}

// --- PHẦN 2: MÀN HÌNH MENU ---
class MenuScreen extends StatefulWidget {
  const MenuScreen({super.key});

  @override
  State<MenuScreen> createState() => _MenuScreenState();
}

class _MenuScreenState extends State<MenuScreen> {
  late Future<List<MenuItem>> futureMenuItems;

  @override
  void initState() {
    super.initState();
    futureMenuItems = ApiService.fetchMenuItems();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Menu - 1771020152"), // Thay MSV của bạn
        backgroundColor: Colors.orange,
      ),
      body: FutureBuilder<List<MenuItem>>(
        future: futureMenuItems,
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return const Center(child: CircularProgressIndicator());
          } else if (snapshot.hasError) {
            return Center(child: Text("Lỗi: ${snapshot.error}"));
          } else if (!snapshot.hasData || snapshot.data!.isEmpty) {
            return const Center(child: Text("Không có món ăn nào"));
          }

          // Hiển thị dạng lưới (Grid)
          return GridView.builder(
            padding: const EdgeInsets.all(10),
            gridDelegate: const SliverGridDelegateWithFixedCrossAxisCount(
              crossAxisCount: 2, // 2 cột
              childAspectRatio: 0.75, // Tỷ lệ khung hình
              crossAxisSpacing: 10,
              mainAxisSpacing: 10,
            ),
            itemCount: snapshot.data!.length,
            itemBuilder: (context, index) {
              final item = snapshot.data![index];
              return Card(
                elevation: 3,
                shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(10)),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    // Ảnh món ăn
                    Expanded(
                      child: Container(
                        width: double.infinity,
                        decoration: BoxDecoration(
                          color: Colors.grey[200],
                          borderRadius: const BorderRadius.vertical(top: Radius.circular(10)),
                        ),
                        child: Icon(Icons.fastfood, size: 50, color: Colors.orange),
                        // Nếu có ảnh thật online thì dùng: Image.network(item.imageUrl)
                      ),
                    ),
                    // Thông tin món
                    Padding(
                      padding: const EdgeInsets.all(8.0),
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Text(
                            item.name,
                            style: const TextStyle(fontWeight: FontWeight.bold, fontSize: 16),
                            maxLines: 1,
                            overflow: TextOverflow.ellipsis,
                          ),
                          const SizedBox(height: 4),
                          Text(
                            "${item.price} VND",
                            style: const TextStyle(color: Colors.green, fontWeight: FontWeight.bold),
                          ),
                          const SizedBox(height: 4),
                          // Hiển thị nhãn Cay/Chay
                          Row(
                            children: [
                              if (item.isSpicy) 
                                const Icon(Icons.local_fire_department, color: Colors.red, size: 16),
                              if (item.isVegetarian) 
                                const Icon(Icons.grass, color: Colors.green, size: 16),
                            ],
                          )
                        ],
                      ),
                    ),
                  ],
                ),
              );
            },
          );
        },
      ),
    );
  }
}