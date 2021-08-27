<template>
  <div class="page">
        <EmployeeList/>
    </div>
</template>

<script>

import EmployeeList from '../../view/employee/EmployeeList.vue'

export default {
  name: "Content",
  components: {
   EmployeeList
  }
};
</script>
