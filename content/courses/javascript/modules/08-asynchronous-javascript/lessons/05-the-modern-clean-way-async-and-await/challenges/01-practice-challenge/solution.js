async function loadUserData() {
  try {
    let user = await fetchUser(1);
    console.log('User:', user.name);
    
    let posts = await fetchPosts(user.id);
    console.log('Posts:', posts.length);
    
    return posts;
  } catch (error) {
    console.log('Error:', error);
  }
}