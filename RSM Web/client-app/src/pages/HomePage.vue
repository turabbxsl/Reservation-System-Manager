<template>
  <div class="homepage">
    
    <section class="services-section container py-5">
      <h2 class="text-center mb-4">Xidmətlərimiz</h2>
      <div class="row g-4">
        <div
          v-for="(service, index) in services"
          :key="index"
          class="col-12 col-sm-6 col-md-4 col-lg-3"
        >
          <div class="card h-100 shadow-sm service-card">
            <div class="card-body text-center">
              <i :class="service.icon + ' fs-2 mb-3'"></i>
              <h5 class="card-title">{{ service.name }}</h5>
              <p class="card-text">{{ service.description }}</p>
            </div>
          </div>
        </div>
      </div>
    </section>

    <section class="reviews-section container py-5">
      <h2 class="text-center mb-4">Müştəri Rəyləri</h2>
      <div class="row g-4">
        <div
          v-for="(review, index) in reviews"
          :key="index"
          class="col-12 col-md-6"
        >
          <div class="card shadow-sm p-3 review-box">
            <blockquote class="blockquote mb-4">
              “{{ review.comment }}”
            </blockquote>
            <footer class="blockquote-footer">
              {{ review.name }} — 
              <span class="text-warning">
                <i v-for="i in review.rating" :key="i" class="bi bi-star-fill"></i>
              </span>
            </footer>
          </div>
        </div>
      </div>
    </section>
  </div>
</template>

<script>
export default {
  name: "HomePage",
  data() {
    return {
      services: [
        { name: "Saç düzümü", description: "Peşəkar stilistlər", icon: "bi bi-scissors" },
        { name: "Diş müayinəsi", description: "Diş həkimi xidməti", icon: "bi bi-tooth" },
        { name: "Mesaj", description: "Sms xəbərdarlıq", icon: "bi bi-spa" },
        { name: "Psixoloq", description: "Məsləhət xidməti", icon: "bi bi-person-lines-fill" },
        { name: "Gözəllik salonu", description: "Makiyaj və daha çox", icon: "bi bi-brush" },
        { name: "Uşaq həkimi", description: "Pediatr xidmətləri", icon: "bi bi-heart-pulse" },
        { name: "Fizioterapiya", description: "Müalicəvi hərəkətlər", icon: "bi bi-person-walking" },
        { name: "Ortopediya", description: "Sümük və əzələ problemləri", icon: "bi bi-person-standing-dress" },
      ],
      reviews: [
        { name: "Aygün M.", comment: "Çox məmnun qaldım, hər şey əla idi!", rating: 5 },
        { name: "Tural Ə.", comment: "Peşəkar və gülərüz personal.", rating: 4 },
        { name: "Nərgiz R.", comment: "Vaxtında və keyfiyyətli xidmət.", rating: 5 },
      ],
    };
  }
};
</script>

<style scoped src="@/assets/css/pages/homepage.css"></style>
