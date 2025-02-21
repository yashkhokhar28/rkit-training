# **Step-by-Step Guide: Installing MyDumper 0.11.3 with MySQL 8.0.27 on Ubuntu 20.04**

This guide will walk you through installing MyDumper 0.11.3, ensuring compatibility with MySQL 8.0.27, using the following files:

- `libmysqlclient21_8.0.27-1ubuntu20.04_amd64.deb` (runtime library) [Download](https://downloads.mysql.com/archives/get/p/23/file/libmysqlclient21_8.0.27-1ubuntu20.04_amd64.deb)
- `libmysqlclient-dev_8.0.27-1ubuntu20.04_amd64.deb` (development files including `mysql_config`) [Download](https://downloads.mysql.com/archives/get/p/23/file/libmysqlclient21_8.0.27-1ubuntu20.04_amd64.deb)
- `mydumper-0.11.3.tar.gz` (MyDumper source tarball) [Download](https://github.com/mydumper/mydumper/archive/refs/tags/v0.11.3.tar.gz)

---

## **Step 1: Remove Any Existing MySQL Client Development Packages and MyDumper**

Before proceeding, ensure no conflicting MySQL client versions or MyDumper installations exist.

### **1. Remove Existing MySQL Client Development Packages**

```bash
sudo apt remove --purge libmysqlclient-dev libmysqlclient21
sudo apt autoremove
```

### **2. Remove Any Existing MyDumper Installation**

If MyDumper was previously installed (via `apt` or from source), remove its binaries:

```bash
sudo rm -f /usr/local/bin/mydumper /usr/local/bin/myloader
```

---

## **Step 2: Install MySQL 8.0.27 Client Libraries**

### **1. Install the MySQL Client Runtime Library**

Ensure you are in the directory where the `.deb` files are located (e.g., `/home/ubuntu`):

```bash
sudo dpkg -i libmysqlclient21_8.0.27-1ubuntu20.04_amd64.deb
```

### **2. Install the MySQL Client Development Package**

```bash
sudo dpkg -i libmysqlclient-dev_8.0.27-1ubuntu20.04_amd64.deb
```

### **3. Fix Any Dependency Issues**

If `dpkg` reports missing dependencies, resolve them with:

```bash
sudo apt-get install -f
```

### **4. Verify MySQL Client Installation**

Check the installed version of `mysql_config`:

```bash
mysql_config --version
```

Expected output:

```
8.0.27
```

---

## **Step 3: Extract the MyDumper Source Code**

### **1. Extract the Tarball**

Navigate to the directory containing `mydumper-0.11.3.tar.gz` and extract it:

```bash
tar xzf mydumper-0.11.3.tar.gz
cd mydumper-0.11.3
```

---

## **Step 4: Configure the Build with CMake**

### **1. Create and Enter a Build Directory**

It is best practice to build out-of-source:

```bash
mkdir build
cd build
```

### **2. Run CMake**

Run the following command to configure the build:

```bash
cmake -DMYSQL_CONFIG=/usr/bin/mysql_config \
      -DMYSQL_LIBRARIES_ssl="/usr/lib/x86_64-linux-gnu/libssl.so" \
      -DMYSQL_LIBRARIES_crypto="/usr/lib/x86_64-linux-gnu/libcrypto.so" \
      ..
```

**Notes:**

- This ensures that MyDumper uses MySQL 8.0.27’s `mysql_config`.
- It specifies the correct OpenSSL libraries.
- If you see warnings related to Sphinx documentation generation, they can be ignored. Alternatively, install Sphinx with:
  ```bash
  sudo apt install python3-sphinx
  ```

### **3. Verify CMake Output**

Ensure:

- MySQL 8.0.27 is detected correctly.
- No missing dependencies are reported.

---

## **Step 5: Compile MyDumper**

### **1. Build Using `make`**

Use all available CPU cores to speed up compilation:

```bash
make -j$(nproc)
```

---

## **Step 6: Install MyDumper (Optional)**

To install MyDumper system-wide:

```bash
sudo make install
```

This installs `mydumper` and `myloader` in `/usr/local/bin`.

---

## **Step 7: Verify Installation**

### **1. Check MyDumper Version**

```bash
mydumper --version
```

Expected output:

```
mydumper 0.11.2, built against MySQL 8.0.27
```

### **2. Check MyLoader Version**

```bash
myloader --version
```

Expected output:

```
myloader 0.11.2, built against MySQL 8.0.27
```
