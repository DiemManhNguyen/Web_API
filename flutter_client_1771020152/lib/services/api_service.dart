import 'dart:convert';
import 'package:http/http.dart' as http;
import '../models/menu_item.dart';

class ApiService {
  // QUAN TRỌNG: Thay 44397 bằng PORT thật trên máy bạn
  // 10.0.2.2 là localhost của máy tính khi nhìn từ máy ảo Android
  static const String baseUrl = "https://localhost:44397/api"; 

  static Future<List<MenuItem>> fetchMenuItems() async {
    try {
      final response = await http.get(Uri.parse('$baseUrl/menu-items'));

      if (response.statusCode == 200) {
        final jsonResponse = jsonDecode(response.body);
        // Dựa vào cấu trúc trả về: { "success": true, "data": [...] }
        final List<dynamic> data = jsonResponse['data'];
        
        return data.map((json) => MenuItem.fromJson(json)).toList();
      } else {
        throw Exception('Lỗi tải dữ liệu: ${response.statusCode}');
      }
    } catch (e) {
      throw Exception('Không kết nối được Server: $e');
    }
  }
}