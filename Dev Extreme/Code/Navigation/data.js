const peripheralData = [
  {
    id: "1",
    name: "Keyboards",
    icon: "fas fa-keyboard",
    expanded: true, // This item will be expanded initially
    items: [
      {
        id: "1_1",
        name: "Mechanical Keyboard RGB",
        price: 120,
        icon: "fas fa-keyboard",
        selected: true, // This item will be pre-selected
      },
      {
        id: "1_2",
        name: "Membrane Keyboard Slim",
        price: 45,
        icon: "fas fa-keyboard",
      },
    ],
  },
  {
    id: "2",
    name: "Mice",
    icon: "fas fa-mouse",
    hasItems: true, // Indicates it has sub-items (useful for dynamic loading)
    items: [
      {
        id: "2_1",
        name: "Wireless Gaming Mouse",
        icon: "fas fa-mouse",
        price: 65,
        template: (itemData) =>
          `<i class='${itemData.icon}'></i> ${itemData.name} - $${itemData.price}`,
      },
      {
        id: "2_2",
        name: "Optical Mouse Basic",
        icon: "fas fa-mouse",
        price: 20,
        disabled: true, // This item will be disabled
      },
      {
        id: "2_3",
        name: "Ergonomic Mouse Pro",
        icon: "fas fa-mouse",
        price: 80,
      },
      {
        id: "2_4",
        name: "Trackball Mouse (Out of Stock)",
        icon: "fas fa-mouse",
        price: 55,
        disabled: true,
      },
    ],
  },
  {
    id: "3",
    name: "Headsets",
    icon: "fas fa-headset",
    items: [
      {
        id: "3_1",
        name: "Wired",
        icon: "fas fa-headset",
        items: [
          {
            id: "3_1_1",
            name: "Stereo Headset Basic",
            icon: "fas fa-headset",
            price: 30,
          },
          {
            id: "3_1_2",
            name: "<span style='color: red;'>🔥 Gaming Headset (Sale!)</span>",
            icon: "fas fa-headset",
            price: 150,
          },
        ],
      },
      {
        id: "3_2",
        name: "Wireless",
        icon: "fas fa-headset",
        items: [
          {
            id: "3_2_1",
            name: "Bluetooth Headset Pro",
            icon: "fas fa-headset",
            price: 90,
          },
          {
            id: "3_2_2",
            name: "Gaming Headset Surround",
            icon: "fas fa-headset",
            price: 150,
          },
        ],
      },
    ],
  },
  {
    id: "4",
    name: "Webcams",
    icon: "fas fa-camera",
    items: [
      {
        id: "4_1",
        name: "HD Webcam 720p",
        icon: "fas fa-camera",
        price: 40,
      },
      {
        id: "4_2",
        name: "FullHD Webcam 1080p",
        icon: "fas fa-camera",
        price: 75,
        visible: false,
      },
    ],
  },
  {
    id: "5",
    name: "Monitors",
    icon: "fas fa-tv",
    items: [
      {
        id: "5_1",
        name: "24 inch Full HD",
        icon: "fas fa-tv",
        price: 150,
      },
      {
        id: "5_2",
        name: "27 inch 4K UHD",
        icon: "fas fa-tv",
        price: 300,
      },
    ],
  },
  {
    id: "6",
    name: "Printers",
    icon: "fas fa-print",
    items: [
      {
        id: "6_1",
        name: "Laser Printer",
        icon: "fas fa-print",
        price: 200,
      },
      {
        id: "6_2",
        name: "Inkjet Printer",
        icon: "fas fa-print",
        price: 100,
      },
    ],
  },
  {
    id: "7",
    name: "Scanners",
    icon: "fas fa-scan",
    items: [
      {
        id: "7_1",
        name: "Flatbed Scanner",
        icon: "fas fa-scan",
        price: 120,
      },
      {
        id: "7_2",
        name: "Sheetfed Scanner",
        icon: "fas fa-scan",
        price: 180,
      },
    ],
  },
  {
    id: "8",
    name: "Speakers",
    icon: "fas fa-volume-up",
    items: [
      {
        id: "8_1",
        name: "Bluetooth Speaker",
        icon: "fas fa-volume-up",
        price: 50,
      },
      {
        id: "8_2",
        name: "Desktop Speaker Set",
        icon: "fas fa-volume-up",
        price: 100,
      },
    ],
  },
  {
    id: "9",
    name: "Microphones",
    icon: "fas fa-microphone",
    items: [
      {
        id: "9_1",
        name: "USB Microphone",
        icon: "fas fa-microphone",
        price: 70,
      },
      {
        id: "9_2",
        name: "Condenser Microphone",
        icon: "fas fa-microphone",
        price: 150,
      },
    ],
  },
  {
    id: "10",
    name: "Cables",
    icon: "fas fa-cable",
    items: [
      {
        id: "10_1",
        name: "HDMI Cable",
        icon: "fas fa-cable",
        price: 10,
      },
      {
        id: "10_2",
        name: "USB-C Cable",
        icon: "fas fa-cable",
        price: 15,
      },
    ],
  },
  {
    id: "11",
    name: "Docking Stations",
    icon: "fas fa-dock",
    items: [
      {
        id: "11_1",
        name: "USB-C Dock",
        icon: "fas fa-dock",
        price: 100,
      },
      {
        id: "11_2",
        name: "Thunderbolt Dock",
        icon: "fas fa-dock",
        price: 200,
      },
    ],
  },
  {
    id: "12",
    name: "Projectors",
    icon: "fas fa-projector",
    items: [
      {
        id: "12_1",
        name: "Portable Projector",
        icon: "fas fa-projector",
        price: 400,
      },
      {
        id: "12_2",
        name: "4K Projector",
        icon: "fas fa-projector",
        price: 2000,
      },
    ],
  },
  {
    id: "13",
    name: "External Hard Drives",
    icon: "fas fa-hdd",
    items: [
      {
        id: "13_1",
        name: "1TB External HDD",
        icon: "fas fa-hdd",
        price: 60,
      },
      {
        id: "13_2",
        name: "2TB External HDD",
        icon: "fas fa-hdd",
        price: 100,
      },
    ],
  },
  {
    id: "14",
    name: "USB Flash Drives",
    icon: "fas fa-usb",
    items: [
      {
        id: "14_1",
        name: "16GB USB Flash Drive",
        icon: "fas fa-usb",
        price: 10,
      },
      {
        id: "14_2",
        name: "64GB USB Flash Drive",
        icon: "fas fa-usb",
        price: 20,
      },
    ],
  },
  {
    id: "15",
    name: "Memory Cards",
    icon: "fas fa-memory",
    items: [
      {
        id: "15_1",
        name: "32GB SD Card",
        icon: "fas fa-memory",
        price: 15,
      },
      {
        id: "15_2",
        name: "128GB SD Card",
        icon: "fas fa-memory",
        price: 40,
      },
    ],
  },
  {
    id: "16",
    name: "Laptop Stands",
    icon: "fas fa-laptop",
    items: [
      {
        id: "16_1",
        name: "Adjustable Laptop Stand",
        icon: "fas fa-laptop",
        price: 30,
      },
      {
        id: "16_2",
        name: "Portable Laptop Stand",
        icon: "fas fa-laptop",
        price: 25,
      },
    ],
  },
  {
    id: "17",
    name: "Cooling Pads",
    icon: "fas fa-fan",
    items: [
      {
        id: "17_1",
        name: "Laptop Cooling Pad",
        icon: "fas fa-fan",
        price: 20,
      },
      {
        id: "17_2",
        name: "Gaming Laptop Cooling Pad",
        icon: "fas fa-fan",
        price: 35,
      },
    ],
  },
  {
    id: "18",
    name: "Stylus Pens",
    icon: "fas fa-pen",
    items: [
      {
        id: "18_1",
        name: "Active Stylus Pen",
        icon: "fas fa-pen",
        price: 50,
      },
      {
        id: "18_2",
        name: "Capacitive Stylus Pen",
        icon: "fas fa-pen",
        price: 20,
      },
    ],
  },
  {
    id: "19",
    name: "Graphic Tablets",
    icon: "fas fa-tablet",
    items: [
      {
        id: "19_1",
        name: "Beginner Graphic Tablet",
        icon: "fas fa-tablet",
        price: 70,
      },
      {
        id: "19_2",
        name: "Professional Graphic Tablet",
        icon: "fas fa-tablet",
        price: 250,
      },
    ],
  },
  {
    id: "20",
    name: "Smartphone Accessories",
    icon: "fas fa-mobile",
    items: [
      {
        id: "20_1",
        name: "Wireless Charger",
        icon: "fas fa-mobile",
        price: 30,
      },
      {
        id: "20_2",
        name: "Phone Stand",
        icon: "fas fa-mobile",
        price: 10,
      },
    ],
  },
  {
    id: "21",
    name: "Tablet Accessories",
    icon: "fas fa-tablet-alt",
    items: [
      {
        id: "21_1",
        name: "Tablet Case",
        icon: "fas fa-tablet-alt",
        price: 20,
      },
      {
        id: "21_2",
        name: "Bluetooth Keyboard for Tablet",
        icon: "fas fa-tablet-alt",
        price: 40,
      },
    ],
  },
  {
    id: "22",
    name: "VR Headsets",
    icon: "fas fa-vr-cardboard",
    items: [
      {
        id: "22_1",
        name: "Basic VR Headset",
        icon: "fas fa-vr-cardboard",
        price: 100,
      },
      {
        id: "22_2",
        name: "Advanced VR Headset",
        icon: "fas fa-vr-cardboard",
        price: 300,
      },
    ],
  },
  {
    id: "23",
    name: "Game Controllers",
    icon: "fas fa-gamepad",
    items: [
      {
        id: "23_1",
        name: "Wireless Game Controller",
        icon: "fas fa-gamepad",
        price: 60,
      },
      {
        id: "23_2",
        name: "Wired Game Controller",
        icon: "fas fa-gamepad",
        price: 30,
      },
    ],
  },
  {
    id: "24",
    name: "Smartwatches",
    icon: "fas fa-watch",
    items: [
      {
        id: "24_1",
        name: "Fitness Smartwatch",
        icon: "fas fa-watch",
        price: 120,
      },
      {
        id: "24_2",
        name: "Luxury Smartwatch",
        icon: "fas fa-watch",
        price: 300,
      },
    ],
  },
  {
    id: "25",
    name: "Smart Home Devices",
    icon: "fas fa-home",
    items: [
      {
        id: "25_1",
        name: "Smart Light Bulb",
        icon: "fas fa-lightbulb",
        price: 15,
      },
      {
        id: "25_2",
        name: "Smart Thermostat",
        icon: "fas fa-thermometer",
        price: 120,
      },
    ],
  },
  {
    id: "26",
    name: "Drones",
    icon: "fas fa-drone",
    items: [
      {
        id: "26_1",
        name: "Mini Drone",
        icon: "fas fa-drone",
        price: 100,
      },
      {
        id: "26_2",
        name: "Professional Drone",
        icon: "fas fa-drone",
        price: 800,
      },
    ],
  },
  {
    id: "27",
    name: "3D Printers",
    icon: "fas fa-print",
    items: [
      {
        id: "27_1",
        name: "Basic 3D Printer",
        icon: "fas fa-print",
        price: 300,
      },
      {
        id: "27_2",
        name: "Advanced 3D Printer",
        icon: "fas fa-print",
        price: 1500,
      },
    ],
  },
  {
    id: "28",
    name: "Robot Vacuums",
    icon: "fas fa-robot",
    items: [
      {
        id: "28_1",
        name: "Basic Robot Vacuum",
        icon: "fas fa-robot",
        price: 200,
      },
      {
        id: "28_2",
        name: "Advanced Robot Vacuum",
        icon: "fas fa-robot",
        price: 500,
      },
    ],
  },
  {
    id: "29",
    name: "Security Cameras",
    icon: "fas fa-camera",
    items: [
      {
        id: "29_1",
        name: "Indoor Security Camera",
        icon: "fas fa-camera",
        price: 50,
      },
      {
        id: "29_2",
        name: "Outdoor Security Camera",
        icon: "fas fa-camera",
        price: 100,
      },
    ],
  },
  {
    id: "30",
    name: "Fitness Trackers",
    icon: "fas fa-heartbeat",
    items: [
      {
        id: "30_1",
        name: "Basic Fitness Tracker",
        icon: "fas fa-heartbeat",
        price: 50,
      },
      {
        id: "30_2",
        name: "Advanced Fitness Tracker",
        icon: "fas fa-heartbeat",
        price: 100,
      },
    ],
  },
  {
    id: "31",
    name: "Electric Scooters",
    icon: "fas fa-scooter",
    items: [
      {
        id: "31_1",
        name: "Basic Electric Scooter",
        icon: "fas fa-scooter",
        price: 300,
      },
      {
        id: "31_2",
        name: "Advanced Electric Scooter",
        icon: "fas fa-scooter",
        price: 600,
      },
    ],
  },
  {
    id: "32",
    name: "Portable Power Banks",
    icon: "fas fa-battery-full",
    items: [
      {
        id: "32_1",
        name: "10000mAh Power Bank",
        icon: "fas fa-battery-full",
        price: 25,
      },
      {
        id: "32_2",
        name: "20000mAh Power Bank",
        icon: "fas fa-battery-full",
        price: 50,
      },
    ],
  },
  {
    id: "33",
    name: "Wireless Chargers",
    icon: "fas fa-charging-station",
    items: [
      {
        id: "33_1",
        name: "Basic Wireless Charger",
        icon: "fas fa-charging-station",
        price: 20,
      },
      {
        id: "33_2",
        name: "Fast Wireless Charger",
        icon: "fas fa-charging-station",
        price: 40,
      },
    ],
  },
  {
    id: "34",
    name: "Smart Glasses",
    icon: "fas fa-glasses",
    items: [
      {
        id: "34_1",
        name: "Basic Smart Glasses",
        icon: "fas fa-glasses",
        price: 200,
      },
      {
        id: "34_2",
        name: "Advanced Smart Glasses",
        icon: "fas fa-glasses",
        price: 400,
      },
    ],
  },
  {
    id: "35",
    name: "Digital Cameras",
    icon: "fas fa-camera-retro",
    items: [
      {
        id: "35_1",
        name: "Compact Digital Camera",
        icon: "fas fa-camera-retro",
        price: 150,
      },
      {
        id: "35_2",
        name: "DSLR Camera",
        icon: "fas fa-camera-retro",
        price: 600,
      },
    ],
  },
  {
    id: "36",
    name: "Action Cameras",
    icon: "fas fa-video",
    items: [
      {
        id: "36_1",
        name: "Basic Action Camera",
        icon: "fas fa-video",
        price: 100,
      },
      {
        id: "36_2",
        name: "4K Action Camera",
        icon: "fas fa-video",
        price: 200,
      },
    ],
  },
  {
    id: "37",
    name: "Smart Rings",
    icon: "fas fa-ring",
    items: [
      {
        id: "37_1",
        name: "Basic Smart Ring",
        icon: "fas fa-ring",
        price: 100,
      },
      {
        id: "37_2",
        name: "Advanced Smart Ring",
        icon: "fas fa-ring",
        price: 200,
      },
    ],
  },
];
