Trả lời phỏng vấn test1.cs
  A. CƠ chế của Unity khi gameObject destroy, thì object đó không thực sự bị xoá đi khỏi ram khi vẫn còn 1 Object nào tham chiếu đến nó, nên ta vẫn có thể truy cập vào biến health của enemy.
  B. Cơ chế kiểm tra null bằng việc sử dụng ( == null ) của unity đã được override lại nên cơ chế đó unity sẽ trả về null khi game object bị xoá, để kiểm tra ref cảu enemy 1 cách chính xác ta sẽ sử dụng (Object.ReferenceEquals(enemy, null)) để thay thế
