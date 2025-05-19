#!/bin/bash

MYSQL_USER="root"
DB_NAME="test_db_1"
mysql_cmd="mysql -u $MYSQL_USER"

# Ask user once how many records to insert into all tables
read -p "Enter number of records to insert into each orders table (orders_1 to orders_10): " ROW_COUNT

# Validate input
if [[ -z "$ROW_COUNT" || "$ROW_COUNT" -le 0 ]]; then
  echo "❌ Invalid row count entered. Exiting."
  exit 1
fi

# Loop over tables orders_1 to orders_10
for tid in {1..10}; do
  TABLE_NAME="orders_${tid}"
  echo "🚀 Inserting $ROW_COUNT records into $TABLE_NAME..."

  for ((i=1; i<=ROW_COUNT; i++)); do
    if (( i % 500 == 1 )); then
      echo "Inserting rows $i - $((i+499<ROW_COUNT ? i+499 : ROW_COUNT))..."
    fi

    ORDER_ID="OID${tid}_${i}"
    DATE="2025-05-19"
    STATUS="Processed"
    FULFILMENT="FBM"
    SALES_CHANNEL="Amazon"
    SHIP_SERVICE="Standard"
    STYLE="Modern"
    SKU="SKU${i}"
    CATEGORY="Apparel"
    SIZE="M"
    ASIN="B00EXAMPLE${i}"
    COURIER_STATUS="In Transit"
    QTY=$(( (i % 5) + 1 ))
    CURRENCY="USD"
    AMOUNT=$(printf "%.2f" "$(echo "$QTY * 19.99" | bc)")
    SHIP_CITY="New York"
    SHIP_STATE="NY"
    POSTAL_CODE="10001"
    COUNTRY="USA"
    PROMO_IDS="PROMO${i}"
    B2B="False"
    FULFILLED_BY="Amazon"

    $mysql_cmd "$DB_NAME" -e "
      INSERT INTO $TABLE_NAME (
        \`index\`, Order_ID, Date, Status, Fulfilment, Sales_Channel, Ship_Service_Level,
        Style, SKU, Category, Size, ASIN, Courier_Status, Qty, Currency, Amount,
        Ship_City, Ship_State, Ship_Postal_Code, Ship_Country, Promotion_IDs,
        B2B, Fulfilled_By
      ) VALUES (
        $i, '$ORDER_ID', '$DATE', '$STATUS', '$FULFILMENT', '$SALES_CHANNEL', '$SHIP_SERVICE',
        '$STYLE', '$SKU', '$CATEGORY', '$SIZE', '$ASIN', '$COURIER_STATUS', $QTY, '$CURRENCY', $AMOUNT,
        '$SHIP_CITY', '$SHIP_STATE', '$POSTAL_CODE', '$COUNTRY', '$PROMO_IDS',
        '$B2B', '$FULFILLED_BY'
      );
    "
  done

  echo "✅ Done inserting into $TABLE_NAME."
done

echo "🎉 All inserts complete."
