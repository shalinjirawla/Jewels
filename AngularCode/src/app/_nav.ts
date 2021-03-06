interface NavAttributes {
  [propName: string]: any;
}
interface NavWrapper {
  attributes: NavAttributes;
  element: string;
}
interface NavBadge {
  text: string;
  variant: string;
}
interface NavLabel {
  class?: string;
  variant: string;
}

export interface NavData {
  name?: string;
  url?: string;
  icon?: string;
  badge?: NavBadge;
  title?: boolean;
  children?: NavData[];
  variant?: string;
  attributes?: NavAttributes;
  divider?: boolean;
  class?: string;
  label?: NavLabel;
  wrapper?: NavWrapper;
}

export const navItems: NavData[] = [
 
  {
    title: true,
    name: 'Main'
  },
  {
    name: 'Dashboard',
    url: '/dashboard',
    icon: 'icon-speedometer',
  },
  
  {
    name: 'Customers',
    url: '/customers',
    icon: 'fa fa-group',
    children: [
      {
        name: 'Customer',
        url: '/customers/customer',
        icon: 'icon-user'
      },
      {
        name: 'Customer Group',
        url: '/customers/customergroup',
        icon: 'icon-user'
      },
      {
        name: 'Import',
        url: '/base/carousels',
        icon: 'icon-cloud-download'
      },
      
    ]
  },
  {
    name: 'Suppliers',
    url: '/suppliers',
    icon: 'icon-people',
    children: [
      {
        name: 'Suppliers',
        url: '/suppliers/supplier',
        icon: 'icon-user'
      },
      {
        name: 'Import',
        url: '/base/carousels',
        icon: 'icon-cloud-download'
      },
     
    ]
  },
  {
    name: 'Products',
    url: '/products',
    icon: 'fa fa-product-hunt',
    children: [
      {
        name: 'Product',
        url: '/products/product',
        icon: 'icon-paypal'
      },
      {
        name: 'Raw Materials',
        url: '/products/raw-materials',
        icon: 'fa fa-industry'
      },
      {
        name: 'Services',
        url: '/products/service',
        icon: 'icon-fire'
      },
      {
        name: 'Categories',
        url: '/products/categories',
        icon: 'icon-vector'
      },
      {
        name: 'Brands',
        url: '/products/brand',
        icon: 'icon-handbag'
      },
    ]
  },
  {
    title: true,
    name: 'Operators'
  },
  {
    name: 'Sales',
    url: '/sales',
    icon: 'cui-cart icons',
    children: [
      {
        name: 'Quotations',
        url: '/sales/quotations',
        icon: 'cui-bookmark icons'
      },
      {
        name: 'Sales Orders',
        url: '/sales/saleOrder',
        icon: 'fa fa-shopping-cart'
      },
      {
        name: 'Sales Imports',
        url: '/buttons/brand-buttons',
        icon: 'fa fa-tags'
      }
    ]
  },
  
  {
    name: 'Fulfillment',
    url: '/notifications',
    icon: 'fa fa-truck',
    children: [
      {
        name: 'Shipments (Standard)',
        url: '/notifications/alerts',
        icon: 'fa fa-star'
      },
      {
        name: 'Shipments (Dropship)',
        url: '/notifications/badges',
        icon: 'fa fa-dropbox'
      },
      {
        name: 'Exchange and Return',
        url: '/notifications/modals',
        icon: 'fa fa-renren'
      }
    ]
  },
  {
    name: 'Purchases',
    url: '/purchases',
    icon: 'fa fa-book',
    children: [
      {
        name: 'Purchase Orders',
        url: '/purchases/purchases-order',
        icon: 'fa fa-cart-plus'
      },
      {
        name: 'Purchase Import',
        url: '/notifications/badges',
        icon: 'fa fa-bookmark'
      },
      {
        name: 'Receive Notes',
        url: '/purchases/receive-notes',
        icon: 'icon-bell'
      }
    ]
  }, {
    name: 'Inventory',
    url: '/notifications',
    icon: 'fa fa-th-large',
    children: [
      {
        name: 'Adjustments',
        url: '/notifications/alerts',
        icon: 'fa fa-cart-plus'
      },
      {
        name: 'Transfers',
        url: '/notifications/badges',
        icon: 'fa fa-arrows'
      },
      {
        name: 'Stock Takes',
        url: '/notifications/modals',
        icon: 'fa fa-houzz'
      }
    ]
  },
  {
    divider: true
  },
  {
    title: true,
    name: 'Masters',
  },
  {
    name: 'Basic Setting',
    url: '/masters',
    icon: ' fa fa-cog',
    children: [
      {
        name: 'General Setup',
        url: '/masters/general-setup',
        icon: 'icon-star'
      },
      {
        name: 'Test',
        url: '/masters/test',
        icon: 'icon-star'
      }
    ]
  },
  {
    title: true,
    name: 'Company',
  },
  {
    name: 'Pages',
    url: '/pages',
    icon: 'icon-star',
    children: [
      {
        name: 'Login',
        url: '/login',
        icon: 'icon-star'
      }
    ]
  },{
    title: true,
    name: 'Reports',
  },
  {
    name: 'Pages',
    url: '/pages',
    icon: 'icon-star',
    children: [
      {
        name: 'Login',
        url: '/login',
        icon: 'icon-star'
      }
    ]
  },
  
];
