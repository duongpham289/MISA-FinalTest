<template>
    <div class="navbar-wrapper">
        <div class="header items-center flex">
          <div class="header-left items-center flex">
            <div class="header-icon mi mi-24 mi-three-stripes"></div>
            <div class="header-branch-name">CÔNG TY CỔ PHẦN MISA</div>
            <a class="flex items-center"
              ><div
                class="
                  header-icon
                  mi mi-14 mi-chevron-right
                  header-branch-icon
                  pointer
                "
              ></div
            ></a>
          </div>
          <div class="header-right items-center flex">
            <div class="pointer header-icon">
              <div class="wrap-notify header-menu-icon-simple">
                <div class="mi mi-24 mi-bell__nav wiggle"></div>
              </div>
            </div>

            <div class="dropdown-user flex items-center">
              <div class="user-ava">
                <div class="mi mi-32 mi-user-avatar avatar"></div>
              </div>
              <div class="display-user">Phạm Hải Dương</div>
              <div class="mi mi-14 mi-chevron-down pointer"></div>
            </div>
          </div>
        </div>
      </div>
</template>

<script>
export default {
    name: "Header",
}
</script>