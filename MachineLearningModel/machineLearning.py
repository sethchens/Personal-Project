import numpy as np

# 製造假資料

# Seed是拿來控制隨機性的變數，不是很重要，不懂沒關係
np.random.seed(0)

# 設定樣本數
num_samples = 1000


# 製造隨機的0跟1來代表性別
gender = np.random.randint(0, 2, size=num_samples)
# 製造隨機的身高資料（用常態分佈，且考慮性別）
height = np.random.normal(loc=175, scale=10, size=num_samples)-gender*np.random.randint(3, 10, size=num_samples)
# 體重資料是用身高跟性別算出來的，再加上一點隨機性
weight = np.random.normal(loc=0, scale=10, size=num_samples) + 0.7 * height - 5 * gender - 52.5

# 把身高轉換成以公尺為單位
height = height/100

data = np.column_stack((height, gender, weight))
data[:20]

# 把特徵（X）跟標籤（y）分開
X = data[:, :2]
y = data[:, 2]

# 新增一列都是1的給X
X = np.hstack((X, np.ones((X.shape[0], 1))))